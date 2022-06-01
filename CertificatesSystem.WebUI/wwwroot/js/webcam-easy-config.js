const webcamElement = document.getElementById('webcam');
const canvasElement = document.getElementById('canvas');
const btnTakeSnapshot = document.getElementById('take-snapshot');
const btnChangeWebcamStatus = document.getElementById('change-webcam-status');
const textWebcamStatus = document.querySelector("#change-webcam-status span");
const studentPhoto = document.getElementById('student-photo');

let webcamStatus = false;
const webcam = new Webcam(webcamElement, 'user', canvasElement);

btnChangeWebcamStatus.addEventListener('click', () => {
    if (webcamStatus) {
        webcam.stop();
        webcamStatus = false;
        textWebcamStatus.textContent = "Encender";
        btnChangeWebcamStatus.classList.remove("btn-danger");
        btnChangeWebcamStatus.classList.add("btn-success");
        return;
    }

    webcam.start()
        .then(result => {
            webcamStatus = true;
            textWebcamStatus.textContent = "Apagar";
            btnChangeWebcamStatus.classList.add("btn-danger");
            btnChangeWebcamStatus.classList.remove("btn-success");
        });
});

btnTakeSnapshot.addEventListener('click', () => {
    studentPhoto.src = webcam.snap();
});