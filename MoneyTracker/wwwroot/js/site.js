document.addEventListener('DOMContentLoaded', function () {
    var button = document.getElementById('toggleButton');
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var allCategoriesVisible = false; // Переменная для отслеживания состояния

    // Функция для показа/скрытия select'ов
    function toggleVisibility() {
        allCategoriesVisible = !allCategoriesVisible; // Меняем состояние

        if (allCategoriesVisible) {
            categoryType.classList.add('hidden');
            subcategory.classList.add('hidden');
            button.textContent = 'Hide All Categories'; // Изменение текста кнопки
        } else {
            categoryType.classList.remove('hidden');
            subcategory.classList.remove('hidden');
            button.textContent = 'Show All Categories'; // Изменение текста кнопки
        }
    }

    function filterSubcategories() {
        var selectedCategory = categoryType.value;

        
        incomeOptions.forEach(function (option) {
            option.classList.add('hidden');
        });
        expenseOptions.forEach(function (option) {
            option.classList.add('hidden');
        });

        if (selectedCategory === 'true') {
            incomeOptions.forEach(function (option) {
                option.classList.remove('hidden');
            });
        } else if (selectedCategory === 'false') {
            expenseOptions.forEach(function (option) {
                option.classList.remove('hidden');
            });
        }
    }

    // Событие для кнопки
    button.addEventListener('click', toggleVisibility);

    // Событие для выбора категории
    categoryType.addEventListener('change', filterSubcategories);

    // Начальная проверка видимости при загрузке страницы
    toggleVisibility(); // Скрыть селекты по умолчанию
});
