document.addEventListener('DOMContentLoaded', function () {
    var button = document.getElementById('toggleButton');
    var button2 = document.getElementById('toggleButton2');
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var inputField = document.getElementById('inputCategory');
    var inputVisible = document.getElementById('inputVisible');
    var inputFilter = document.getElementById('defaultFilter');
    var allCategoriesVisible = document.getElementById('categoryVisibility'); // Переменная для отслеживания состояния
    var res = true;
    // Функция для показа/скрытия select'ов
    //function toggleVisibility() {

    //    if (document.getElementById('subcategory')) {
            
    //        inputField.value = '';
    //        button.textContent = 'Hide All Categories'; // Изменение текста кнопки
    //    } else {
    //        inputField.value = 'All';
    //        button.textContent = 'Show All Categories'; // Изменение текста кнопки
    //    }

    //    allCategoriesVisible = !allCategoriesVisible; // Меняем состояние

    //    if (!allCategoriesVisible) {
    //        //categoryType.classList.add('hidden');
    //        //subcategory.classList.add('hidden');
    //        //inputVisible.value = true;
            
    //    } else {
    //        //categoryType.classList.remove('hidden');
    //        //subcategory.classList.remove('hidden');
            
    //        //inputVisible.value = false;
    //    }
    //}
    function clickButton() {
        if (!document.getElementById('subcategory')) {

            inputVisible.value = true;
            inputField.value = '';
            //button.textContent = 'Hide All Categories'; // Изменение текста кнопки
        } else {
            inputVisible.value = false;
            inputField.value = 'All';
            //button.textContent = 'Show All Categories'; // Изменение текста кнопки
        }
    }
    function clickButton2() {
        if (document.getElementById('subcategory')) {

            inputVisible.value = true;
            inputField.value = '';
            //button.textContent = 'Hide All Categories'; // Изменение текста кнопки
        } else {
            inputVisible.value = false;
            inputField.value = 'All';
            //button.textContent = 'Show All Categories'; // Изменение текста кнопки
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
    button.addEventListener('click', clickButton);
    button2.addEventListener('click', clickButton2);

    // Событие для выбора категории
    categoryType.addEventListener('change', filterSubcategories);

    // Начальная проверка видимости при загрузке страницы
    //toggleVisibility(); // Скрыть селекты по умолчанию
});