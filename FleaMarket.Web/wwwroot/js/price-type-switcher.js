$(document).ready(() => {
    $('.js-price-form-group').each(function () {
        let input = $('.js-price-input', this);
        $('#pricetype-price', this).change(function () {
            $(input).prop('type', 'number').val('');
        });

        $('#pricetype-contract', this).change(function () {
            $(input).prop('type', 'hidden').val('');
        });

        $('#pricetype-free', this).change(function () {
            $(input).prop('type', 'hidden').val('0');
        });
    })
})