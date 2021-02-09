$(document).ready(function (e) {
    $('.js-item-publishing-date').each(function () {
        $(this).html(getLocalDate($(this).data('utcDatetime')));
    });
});

function getLocalDate(UTCDate) {
    return new Date(UTCDate + ' UTC').toLocaleString();
}