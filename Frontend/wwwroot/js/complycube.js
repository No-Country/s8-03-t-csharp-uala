function startVerification(dotNetRef, token) {  
    var complycube = ComplyCube.mount({
        token: token,
        containerId: 'complycube-mount',
        language: 'es',
        stages: [
            'intro',
            'userConsentCapture',
            'documentCapture',
            'completion'
        ],
        onComplete: function (data) {
            console.info(data);
            var obj = JSON.stringify(data);
            console.info(obj);
            dotNetRef.invokeMethodAsync('VerificationComplete');
        },
        onModalClose: function () {;
            complycube.updateSettings({ isModalOpen: false });   
        },
        onError: function ({ type, message }) {
            if (type === 'token_expired') {
                
            } else {                
                console.err(message);
            }
        }
    });
}