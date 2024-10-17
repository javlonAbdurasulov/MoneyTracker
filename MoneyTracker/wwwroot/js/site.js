$(document).ready(function () {
    var incomeOptions = '<option value="1">Salary</option><option value="2">Bonus</option><option value="3">Investment</option>';
    var expenseOptions = '<option value="4">Rent</option><option value="5">Utilities</option><option value="6">Groceries</option>';

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

