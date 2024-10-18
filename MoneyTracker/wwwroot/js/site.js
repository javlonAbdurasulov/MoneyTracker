document.addEventListener('DOMContentLoaded', function () {
    var button = document.getElementById('toggleButton');
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var allCategoriesVisible = false; // Переменная для отслеживания состояния
    var inputField = document.getElementById('inputCategory');
    //var filterGroup = document.getElementById('forCategory');
    // Функция для показа/скрытия select'ов
    function toggleVisibility() {

        //var categoryType = document.getElementById('categoryType');
        //// Если элемент существует, удаляем его
        //if (categoryType) {
        //    categoryType.remove();
        //} else {
        //    // Если элемента нет, создаем его
        //    categoryType = document.createElement('select');
        //    categoryType.id = 'categoryType';
        //    categoryType.name = 'MoneyFilter.Category.IsIncome';

        //    // Создаем опцию Income
        //    var incomeOption = document.createElement('option');
        //    incomeOption.value = 'true';
        //    incomeOption.textContent = 'Income';

        //    // Создаем опцию Expense
        //    var expenseOption = document.createElement('option');
        //    expenseOption.value = 'false';
        //    expenseOption.textContent = 'Expense';

        //    // Добавляем опции в select
        //    categoryType.appendChild(incomeOption);
        //    categoryType.appendChild(expenseOption);

        //    // Находим элемент div с классом filter-group forCategory
            

        //    // Добавляем созданный select после label
        //    filterGroup.appendChild(categoryType);
        //}

        //--------------------
        allCategoriesVisible = !allCategoriesVisible; // Меняем состояние

        if (allCategoriesVisible) {
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
    console.log("Initial visibility state: ", allCategoriesVisible);
    toggleVisibility(); // Скрыть селекты по умолчанию
});


