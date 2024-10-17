$(document).ready(function () {
    // Define the options for each category
    var incomeOptions = '<option value="Salary">Salary</option><option value="Bonus">Bonus</option><option value="Investment">Investment</option>';
    var expenseOptions = '<option value="Rent">Rent</option><option value="Utilities">Utilities</option><option value="Groceries">Groceries</option>';

    // When the category changes
    $('#category').change(function () {
        var selectedCategory = $(this).val();

        // Clear the subcategory options
        $('#subcategory').empty();

        // Populate the subcategory options based on the selected category
        if (selectedCategory === 'Income') {
            $('#subcategory').append(incomeOptions);
        } else if (selectedCategory === 'Expense') {
            $('#subcategory').append(expenseOptions);
        }
        // If "All" is selected, leave the subcategory empty
    });
});

