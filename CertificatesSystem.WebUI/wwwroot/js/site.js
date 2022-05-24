// Modal form validations
const inputsCreate = document.querySelectorAll('.input-create');

// Clear inputs value
const clearInputs = () => {
    inputsCreate.forEach((input) => (input.value = ''));

    // Hide invalid feedback
    const activeFeedback = document.querySelectorAll('.is-invalid');
    activeFeedback.forEach((label) => label.classList.remove('is-invalid'));
}

const clearFeedback = (input, feedback) => {
    input.classList.remove('is-invalid');
    feedback.innerHTML = '';
}