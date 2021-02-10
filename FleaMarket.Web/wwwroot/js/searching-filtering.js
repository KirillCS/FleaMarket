$(document).ready(function () {
    let input = $('#js-search-input');
    let clearButton = $('#js-clear-search-button');
    let resultLabel = $('#js-search-result-label');
    let loading = $('#js-loading');
    let noItemLabel = $('#js-no-items-label');
    let itemsList = $('#js-items-list');
    let itemTemplate = $('li:first', itemsList);
    let paginator = ('#paginator');

    initPaginator();

    $(input).keypress(function (e) {
        if (e.keyCode !== 13) {
            return;
        }

        $(resultLabel).hide();
        $(noItemLabel).hide();
        initPaginator();
    });

    $(clearButton).click(function () {
        input.val('');
        $(resultLabel).hide();
        $(noItemLabel).hide();
        initPaginator();
    });

    function initPaginator() {
        $(paginator).pagination({
            dataSource: 'https://localhost:44332/item/api',
            ajax: {
                data: {
                    searchString: $(input).val()
                },
                beforeSend: function () {
                    $("html, body").animate({ scrollTop: 0 }, 200);
                    $(loading).show();
                },
                complete: function () {
                    $(loading).hide();
                }
            },
            locator: 'items',
            totalNumber: 1000,
            totalNumberLocator: function (response) {
                return parseInt(response.totalNumber);
            },
            pageSize: 10,
            pageRange: 1,
            showPageNumbers: true,
            hideWhenLessThanOnePage: true,
            callback: function (data) {
                itemsList.html('');
                if ($(input).val().length) {
                    $(resultLabel).html(`Search result by "${$(input).val()}"`).show();
                }

                if (!data.length) {
                    $(noItemLabel).show();
                } else {
                    $(noItemLabel).hide();
                }

                for (var i = 0; i < data.length; i++) {
                    appendItemToList(itemsList, data[i]);
                }
            }
        });
    }

    function appendItemToList(itemsList, itemData) {
        let coverData = itemData.images[0];

        let currencySymbol = $(itemsList).data('currencySymbol');

        let categories = '';
        if (itemData.categories) {
            for (var i = 0; i < itemData.categories.length; i++) {
                categories += `<a class="ml-0" href="#">${itemData.categories[i]['name']}</a>\n`
            }
        }

        let tradeEnabled = itemData.tradeEnabled ? 'exchange is available' : '';
        let description = itemData.description != null ? itemData.description.replaceAll('\r\n', '<br>') : '';
        let price = itemData.priceType === 0 ? 'Free' : itemData.priceType === 1 ? 'Contract' : parseFloat(itemData.price).toFixed(2) + currencySymbol;
        let publishingDate = getLocalDate(new Date(itemData.publishingDate).toString());

        $(itemTemplate).clone().appendTo(itemsList);
        let item = $('li:last-child', itemsList);
        $(item).html($(item).html()
            .replaceAll('[itemName]', itemData.name)
            .replaceAll('[itemCategories]', categories)
            .replaceAll('[itemTradeEnabled]', tradeEnabled)
            .replaceAll('[itemDescription]', description)
            .replaceAll('[itemPrice]', price)
            .replaceAll('[itemPublishingDate]', publishingDate));
        if (coverData) {
            $('img', item).attr('src', coverData.path);
        }

        $(item).show();
    }
});