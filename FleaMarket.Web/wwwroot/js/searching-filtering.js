$(document).ready(function () {
    let input = $('#js-search-input');
    let clearButton = $('#js-clear-search-button');
    let resultLabel = $('#js-search-result-label');
    let loading = $('#js-loading');
    let itemsList = $('#js-items-list');

    $(input).keypress(function (e) {
        if (e.keyCode !== 13) {
            return;
        }

        search();
    });

    $(clearButton).click(function (e) {
        input.val('');
        search();
    });

    function search() {
        $(resultLabel).hide();
        $(itemsList).html('');
        $(loading).show();
        $.ajax({
            url: `/api/search`,
            type: 'GET',
            data: {
                searchString: $(input).val()
            },
            dataType: 'json',
            complete: function () {
                $(loading).hide();
            },
            success: function (data) {
                if ($(input).val().length) {
                    $(resultLabel).html(`Search result by "${$(input).val()}"`).show();
                }

                for (var i = 0; i < data.length; i++) {
                    appendItemToList(itemsList, data[i]);
                }
            }
        });
    }

    function appendItemToList(itemsList, itemData) {
        let cover = itemData['cover'];
        let item = itemData['item'];

        let currencySymbol = $(itemsList).data('currencySymbol');

        let categories = '';
        if (item['categories']) {
            for (var i = 0; i < item['categories'].length; i++) {
                categories += `<a class="ml-0" href="#">${item['categories'][i]['name']}</a>\n`
            }
        }

        $(itemsList).append(`
            <li class="list-group-item">
                <div class="d-flex align-items-center">
                    <div class="img-cover">
                        <a class="card-link text-wrap" href="#"><img src="${cover['path']}" alt="${item['name']}" /></a>
                    </div>
                    <div class="d-flex justify-content-between flex-column p-0 pl-3 w-100">
                        <h5><a class="card-link text-wrap" href="#">${item['name']}</a></h5>
                        <div class="mb-1 limited-text line-clamp-1">
                            ${categories}
                            ${item['tradeEnabled'] ? 'exchange is available' : ''}
                        </div>
                        <div class="text-black-50 mb-2 limited-text line-clamp-2 hidden-sm">
                            ${item['description'] != null ? item['description'].replaceAll('\r\n', '<br>') : ''}
                        </div>
                        <div class="d-flex flex-row flex-wrap justify-content-between align-items-center">
                            <h5>Price: ${item['priceType'] === 0 ? 'Free' : item['priceType'] === 1 ? 'Contract' : parseFloat(item['price']).toFixed(2) + currencySymbol}</h5>
                            <div class="small text-muted">${getLocalDate(new Date(item['publishingDate']).toString())}</div>
                        </div>
                    </div>
                </div>
            </li>
        `);
    }
});