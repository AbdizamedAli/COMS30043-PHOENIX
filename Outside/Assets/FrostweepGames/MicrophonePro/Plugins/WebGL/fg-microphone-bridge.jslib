var FGMicrophoneLibrary = {

    $CallbacksMap:{},

    devices: function (callback) {
        var callbackName = "devices";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.devices(callbackName);
    },

    end: function(deviceId, callback) {
        var callbackName = "end";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.end(document.getStringFromPtr(deviceId), callbackName);
    },

    getDeviceCaps: function(deviceId) {
        return document.FGUnityMicrophone.getDeviceCaps(document.getStringFromPtr(deviceId));
    },

    isRecording: function(deviceId) {
        return document.FGUnityMicrophone.isRecording(document.getStringFromPtr(deviceId));
    },

    start: function(deviceId, frequency, callback) {
        var callbackName = "start";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.start(document.getStringFromPtr(deviceId), frequency, callbackName);
    },

    requestPermission: function(callback) {
        var callbackName = "requestPermission";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.requestPermission(callbackName);
    },

    isPermissionGranted: function(callback) {
        var callbackName = "isPermissionGranted";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.isPermissionGranted(callbackName);
    },

    isSupported: function() {
        return document.FGUnityMicrophone.isSupported();
    },
    
    setRecordingBufferCallback: function(callback) {
        var callbackName = "setRecordingBufferCallback";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.setRecordingBufferCallback(callbackName);
    },

    getRecordingBuffer: function(callback) {
        var callbackName = "getRecordingBuffer";
        CallbacksMap[callbackName] = callback;
        document.FGUnityMicrophone.getRecordingBuffer(callbackName);
    },

    connectToPeer: function() {
        if(document.peer != null) {
            document.peer.destroy();
            document.peer = null;
        }
        console.log('Creating a new peer');
        // create new Peer object and return it's ID
        // document.peer = new Peer(null, {secure: true, debug: 2}); 
        document.peer = new Peer(); 

        document.peer.on('open', function (id) {
            // return the id of the Peer as a string
            document.hasId = true
            console.log("received peer id: " + id);
            document.peerID = id;
            SendMessage('Voice_Audio_PeerJS', 'sendClientId', id);
        });

        document.peer.on('disconnected', function () {
            console.log("Disconnected from the server");
            document.peer.reconnect();
        });


        // called when a remote Peer tries to connect to you
        document.peer.on('connection', function(conn) {
            console.log("getting called by another peer");
            if(document.connection != null) {
                document.connection.close();
                document.connection = null;
            }

            document.connection = conn;
            document.setupConnection();
        });

        document.peer.on('call', function (call) {
            document.call = call;
            document.call.answer(document.FGUnityMicrophone.localStream);
    
            document.setupCall();
        });
    },

    startConnection: function(receiverIdPointer) {
        if(document.peer == null){
            console.log("Start Connection called without a Peer existing");
            return;
        }

        receiverId = UTF8ToString(receiverIdPointer);

        if(document.connection != null) {
            document.connection.close();
            document.connection = null;
        }

        if(document.hasId) {
            console.log("calling peer with id: " + receiverId);
        
            // document.connection = document.peer.connect(receiverId, {reliable:true});
            document.clientID = receiverId;
            document.connection = document.peer.connect(receiverId);
            document.setupConnection();
        } else {
            console.log('wait for own id');
            document.peer.on('open', function (id) {
                console.log("calling peer with id: " + receiverId);
        
                // document.connection = document.peer.connect(receiverId, {reliable:true});
                document.connection = document.peer.connect(receiverId);
                document.setupConnection();
            });
        }
    },
 
    startCall: function(receiverIdPointer) {
        if(document.peer == null){
            console.log("Start Connection called without a Peer existing");
            return;
        }

        receiverId = UTF8ToString(receiverIdPointer);

        if(document.call != null) {
            document.call.close();
            document.call = null;
        }

        if(document.hasId) {
            console.log("calling peer with id: " + receiverId);
        
            document.call = document.peer.call(receiverId, document.FGUnityMicrophone.localStream);
            document.setupCall();
        } else {
            console.log('wait for own id');
            document.peer.on('open', function (id) {
                console.log("calling peer with id: " + receiverId);
        
                document.call = document.peer.call(receiverId, document.FGUnityMicrophone.localStream);
                document.setupCall();
            });
        }
    },
 
    updateVolume: function(newVolume) { 
        var audio = document.getElementById('audio' + document.peerID); 
        audio.volume = newVolume; 
    }, 

    init: function(version, worklet) {
        if(document.FGUnityMicrophone != undefined)
            return 0;
        document.FGUnityMicrophone = new UnityMicrophone(version, worklet);
        
        function setupConnection() {
            if(document.connection == null) {
                console.log("Setup Connection called without a connection existing");
                return;
            }
            
            console.log('Setting up a new connection');
            // on open will be launch when you successfully connect to the other Peer
            document.connection.on('open', function() {
                console.log('Connected to receiver');
                document.connected = true;
    
                SendMessage('Voice_Audio_PeerJS', 'updateCode', 0);
            });
    
            document.connection.on('error', function(err) {
                // doesn't have documentation on what it returns so just mark the connection as invalid
                console.log("create connection exception: " + err.name + ": " + err.message + "; " + err.stack);
                document.connection.close();
                document.connected = false;
                document.connection = null;
                SendMessage('Voice_Audio_PeerJS', 'updateCode', 1);
            });
        }

        function setupCall() {
            if(document.call == null) {
                console.log("Setup Call called without a call existing");
                return;
            }
    
            // Receive data
            document.call.on('stream', function (stream) {
                console.log('Connected to call');
                document.connected = true;
                var audio = document.createElement('audio');
                audio.autoplay = 'autoplay'
                if (document.clientID == null){
                    audio.id = 'audio' + document.peerID;
                }else{
                    audio.id = 'audio' + document.clientID;

                }
                audio.srcObject = stream;
                document.getElementById('unity-footer').appendChild(audio);
                SendMessage('Voice_Audio_PeerJS', 'updateCode', 0);
            });

            document.call.on('error', function(err) {
                console.log("create call exception: " + err.name + ": " + err.message + "; " + err.stack);
                document.call.close();
                document.call = false;
                document.call= null;

                SendMessage('Voice_Audio_PeerJS', 'updateCode', 1);
            });
        }

        function getStringFromPtr(ptr) {
            if(document.FGUnityMicrophone.unityVersion >= 2021.0){
                return UTF8ToString(ptr);
            } else{
                return UTF8ToString(ptr);
            }
        }

        function getPtrFromString(str){
            var bufferSize = lengthBytesUTF8(str) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(str, buffer, bufferSize);
            return buffer;
        }

        function callUnityCallback(callback, object){
            if(callback == null)
                return;

            var ptrFunc = CallbacksMap[callback];

            if(callback == "setRecordingBufferCallback"){
                var samples = new Float32Array(object.data);
                var buffer = _malloc(samples.length * 4)
                HEAPF32.set(object.data, buffer >> 2)

                if(document.FGUnityMicrophone.unityVersion >= 2021.0){
                    Module['dynCall_vii'](ptrFunc, buffer, object.length);
                } else{
                    Runtime.dynCall('vii', ptrFunc, [buffer, object.length]);
                }

                _free(buffer);
            } else {        
                var json = UnityWebGLTools.objectToJSON(object);
                var buffer = getPtrFromString(json);

                if(document.FGUnityMicrophone.unityVersion >= 2021.0){
                    Module['dynCall_vi'](ptrFunc, buffer);
                } else{
                    Runtime.dynCall('vi', ptrFunc, [buffer]);
                }
                
                _free(buffer);
            }
        }

        document.getPtrFromString = getPtrFromString;
        document.getStringFromPtr = getStringFromPtr;
        document.callUnityCallback = callUnityCallback;
        document.peer = null; 
        document.peerID = null;
        document.clientID = null;
        document.connected = false; 
        document.hasId = false
        document.connection = null; 
        document.connections = []; 
        document.call = null; 

        document.setupConnection = setupConnection; 
        document.setupCall = setupCall; 

        return 1;
    }
};

autoAddDeps(FGMicrophoneLibrary, '$CallbacksMap');
mergeInto(LibraryManager.library, FGMicrophoneLibrary);