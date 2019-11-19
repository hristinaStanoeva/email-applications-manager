$('.preview-btn').click(function (event) {
    const id = $(event.target).attr('data-model-id');
    $('#preview-all-email-modal-' + id).modal('show');

    $('#modal-body-' + id).html(`
        <div class="d-flex justify-content-center">
            <div class="spinner-border mt-3 mb-3" style="color: #EC7424;"></div>
        </div>
`);

    $.get('/email/emailbody?id=' + id, function (data) {
        $('#modal-body-' + id).html(data);
    });
});
