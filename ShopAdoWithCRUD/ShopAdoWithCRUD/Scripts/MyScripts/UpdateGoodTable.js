var currentPage = 1;
var PageCount = $('#inc').data("pagecount");

$('#curPageNumber').text(currentPage);

$('#inc').on('click', function () {
    if (currentPage != PageCount) {
        currentPage++;
        $('#curPageNumber').text(currentPage);
        fetch(`/Home/GoodTable/${currentPage}`)
            .then(res => {
                res.text().then(text => {
                    if (text) {
                        $("#goodTable").html(text);
                    }
                });
            });
    }
});

$('#decr').on('click', function () {
    if (currentPage > 1) {
        currentPage--;
        $('#curPageNumber').text(currentPage);
        fetch(`/Home/GoodTable/${currentPage}`)
            .then(res => {
                res.text().then(text => {
                    if (text) {
                        $("#goodTable").html(text);
                    }
                });
            });
    }
});