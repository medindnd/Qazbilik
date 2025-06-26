document.addEventListener('DOMContentLoaded', function() {
    // Обработка формы записи
    const form = document.getElementById('enrollForm');
    if (form) {
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            alert('Ваша заявка принята!');
            form.reset();
        });
    }
    
    // Выбор курса из URL
    const urlParams = new URLSearchParams(window.location.search);
    const course = urlParams.get('course');
    if (course && document.getElementById('courseSelect')) {
        document.getElementById('courseSelect').value = course;
    }
});
