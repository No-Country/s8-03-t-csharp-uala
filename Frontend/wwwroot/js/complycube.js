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
function GenerateToken(obj) {
    const requestBody = {
        clientId: obj,
        referrer: '*://*/*'
    };
    const options = {
        method: 'POST',
        headers: {
            Authorization: 'test_d2o3U25DeXNEZUVvbUs2cWg6NzkwYThkNzU5NWUzOTM1MGMwZTYwZDJiM2MxNTk3OWYwOTEzYmIzN2EwNWQ2ZjQ0ZjgxYWEyNTQ1MTQ3MDUwMQ==',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)       
    };

    fetch('https://api.complycube.com/v1/tokens', options)
        .then(response => response.json())
        .then(response => {
            return response;
        })
        .catch(err => {
            console.error(err);
            return null; // Return null in case of an error
        });
}
function CreateUser(obj) {
    const options = {
        method: 'POST',
        headers: {
            Authorization: 'test_d2o3U25DeXNEZUVvbUs2cWg6NzkwYThkNzU5NWUzOTM1MGMwZTYwZDJiM2MxNTk3OWYwOTEzYmIzN2EwNWQ2ZjQ0ZjgxYWEyNTQ1MTQ3MDUwMQ==',
            'Content-Type': 'application/json'
        },
        body: obj        
    };

    return fetch('https://api.complycube.com/v1/clients', options)
        .then(response => response.json())
        .then(response => {
            return response; // Return the response object
        })
        .catch(err => {
            console.error(err);
            return null; // Return null in case of an error
        });
}