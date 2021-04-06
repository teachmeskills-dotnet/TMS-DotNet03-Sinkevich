$('#downloadPDF').click(function () {
    domtoimage.toPng(document.getElementById('content'))
        .then(function (blob) {
            var pdf = new jsPDF('l', 'pt', [$('#content').width(), $('#content').height()]);

            pdf.addImage(blob, 'PNG', 0, 0, $('#content').width(), $('#content').height());
            pdf.save("reservations.pdf");

            that.options.api.optionsChanged();
        });
});
