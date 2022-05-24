// Delete button confirm dialog
const deleteElementButton = document.querySelectorAll(".delete-element-form");

deleteElementButton.forEach(form => {
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        Swal.fire({
            title: '¿Está seguro?',
            text: "Si borra el elemento, no podrá recuperarlo",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, borrar'
        }).then((result) => {
            if (result.isConfirmed) {
                form.submit();
            }
        });
    });
});


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