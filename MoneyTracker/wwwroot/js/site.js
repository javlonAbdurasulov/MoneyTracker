    var allCategoriesVisible = false; // Переменная для отслеживания состояния
document.addEventListener('DOMContentLoaded', function () {
    var button = document.getElementById('toggleButton');
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var inputField = document.getElementById('inputCategory');
    
    // Функция для показа/скрытия select'ов
    function toggleVisibility() {

        allCategoriesVisible = !allCategoriesVisible; // Меняем состояние

        if (!allCategoriesVisible) {
            categoryType.classList.add('hidden');
            subcategory.classList.add('hidden');
            button.textContent = 'Hide All Categories'; // Изменение текста кнопки
            inputField.value = 'All';
        } else {
            categoryType.classList.remove('hidden');
            subcategory.classList.remove('hidden');
            button.textContent = 'Show All Categories'; // Изменение текста кнопки
            inputField.value = '';
        }
    }

    // Функция для фильтрации подкатегорий в зависимости от выбора категории
    function filterSubcategories() {
        var selectedCategory = categoryType.value;

        // Скрываем все опции сначала
        incomeOptions.forEach(function (option) {
            option.classList.add('hidden');
        });
        expenseOptions.forEach(function (option) {
            option.classList.add('hidden');
        });

        // Показываем только соответствующие опции
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