document.addEventListener('DOMContentLoaded', function () {
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var allCategoriesVisible = false; // Переменная для отслеживания состояния


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


    // Событие для выбора категории
    categoryType.addEventListener('change', filterSubcategories);
    filterSubcategories();
});


