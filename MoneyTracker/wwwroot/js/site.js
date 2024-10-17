$(document).ready(function () {
    var incomeOptions = '<option value="1">Заработная плата</option><option value="2">Аренда</option><option value="3">Иные приходы</option>';
    var expenseOptions = '<option value="4">Продукты питания</option><option value="5">Транспорт</option><option value="6">Мобильная Связь</option><option value="7">Интернет</option><option value="8">Развлечение</option><option value="9">Другое</option>';
    var typeOptions = '<option value="true">Income</option><option value="false">Expense</option>';

    const categoryType = document.getElementById('categoryType');
    const checkbox = document.getElementById('my-checkbox');
    const hiddenInput = document.getElementById('checkbox-value');
    checkbox.addEventListener('change', function () {
        if (this.checked) hiddenInput.value = 'All';
        else {
            categoryType.innerHTML = '';
            categoryType.innerHTML = typeOptions;
        }
    });

    $('#category').change(function () {
        var selectedCategory = $(this).val();

        $('#subcategory').empty();

        if (selectedCategory === true) {
            $('#subcategory').append(incomeOptions);
        } else if (selectedCategory === false) {
            $('#subcategory').append(expenseOptions);
        }
    });
});

