document.addEventListener('DOMContentLoaded', function () {
    var button = document.getElementById('toggleButton');
    var categoryType = document.getElementById('categoryType');
    var subcategory = document.getElementById('subcategory');
    var incomeOptions = document.querySelectorAll('.income');
    var expenseOptions = document.querySelectorAll('.expense');
    var allCategoriesVisible = localStorage.getItem('allCategoriesVisible') === 'true'; // Сохранение состояния из localStorage

    // Функция для показа/скрытия select'ов
    function toggleVisibility() {
        allCategoriesVisible = !allCategoriesVisible; // Меняем состояние
        console.log("Visibility toggled: ", allCategoriesVisible);

        if (allCategoriesVisible) {
            categoryType.classList.add('hidden');
            subcategory.classList.add('hidden');
            button.textContent = 'Show All Categories'; // Изменение текста кнопки
        } else {
            categoryType.classList.remove('hidden');
            subcategory.classList.remove('hidden');
            button.textContent = 'Hide All Categories'; // Изменение текста кнопки
        }

        // Сохраняем состояние в localStorage
        localStorage.setItem('allCategoriesVisible', allCategoriesVisible);
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
    toggleVisibility(); // Устанавливаем начальную видимость
});
document.getElementById('yourFormId').addEventListener('submit', function (event) {
    event.preventDefault(); // Остановить стандартное поведение формы

    var formData = new FormData(this); // Получаем данные формы

    fetch(this.action, {
        method: 'POST',
        body: formData,
    })
        .then(response => response.text())
        .then(data => {
            // Обновите нужные элементы на странице, используя полученные данные
            document.getElementById('resultContainer').innerHTML = data;
        })
        .catch(error => console.error('Ошибка:', error));
});
