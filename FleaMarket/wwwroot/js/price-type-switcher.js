$(document).ready(() => {
    $('.js-price-form-group').each(function () {
        let form = $(this);
        $('#pricetype-price', this).change(function () {
            $('.js-price-input', form)
                .prop('type', 'number')
                .val('');
        });

        $('#pricetype-contract', this).change(function () {
            $('.js-price-input', form)
                .prop('type', 'hidden')
                .val('');
        });

        $('#pricetype-free', this).change(function () {
            $('.js-price-input', form)
                .prop('type', 'hidden')
                .val('0');
        });
    })
})