var wkhtmlToPdfOptions = require('wkhtmltopdf-nodejs-options-wrapper'),
    PdfApi = require('wkhtmltopdf-nodejs-pdfapi');

var pdfApi = new PdfApi(),
    request = new wkhtmlToPdfOptions.CreateRequest(),
    page = new wkhtmlToPdfOptions.Page();

//let's generate pdf from google.com
page.setInput('http://google.com');
request.addPage(page);
request.setDebug(true); //we want to see all wkhtmltopdf output

pdfApi.createPdf(request, 'google.pdf')
    .then(function (data, debug) {
        console.log('Pdf is generated!');
    }, function (data, debug) {
        console.log('Houston, we have a problem: ' + data);
    });

//after some time we can delete pdf
pdfApi.deletePdf('google.pdf').then(function () {
    console.log('Pdf is deleted');
}, function (error) {
    console.log('Something went wrong: ' + error);
});