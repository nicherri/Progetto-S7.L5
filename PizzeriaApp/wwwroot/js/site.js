$(document).ready(function () {
    $('#deleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var productId = button.data('id');
        var modal = $(this);
        modal.find('.modal-body #deleteProductId').val(productId);
    });

    $('#ingredientModal').on('show.bs.modal', function () {
        $.ajax({
            url: '/Admin/Ingredient/GetAll',
            type: 'GET',
            success: function (data) {
                var ingredientList = $('#ingredientList');
                ingredientList.empty();
                data.forEach(function (ingredient) {
                    ingredientList.append('<li class="list-group-item"><input type="checkbox" value="' + ingredient + '"/> ' + ingredient + '</li>');
                });
            },
            error: function () {
                alert("Errore durante il caricamento degli ingredienti.");
            }
        });
    });

    $('#saveIngredients').on('click', function () {
        var selectedIngredients = [];
        $('#ingredientList input:checked').each(function () {
            selectedIngredients.push($(this).val());
        });
        $('textarea[name="Ingredienti"]').val(selectedIngredients.join(', '));
        $('#ingredientModal').modal('hide');
    });
});
