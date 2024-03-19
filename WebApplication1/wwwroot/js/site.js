//html2canvas(document.querySelector("#capture")).then(canvas => {
//    //document.body.appendChild(canvas)
//    canvas.getContext('2d');

//    //console.log(canvas.height+"  "+canvas.width);

//    let imgData = canvas.toDataURL("image/jpeg", 1.0);
//    let pdf = new jsPDF('p', 'mm', [PDF_Width, PDF_Height]);
//    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);

//    pdf = self.addPdfHeader(pdf);
//    pdf = self.addPdfFooter(pdf, 1);

//    for (let i = 1; i <= totalPDFPages; i++) {
//        pdf.addPage(PDF_Width, PDF_Height);

//        pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4) + top_left_margin, canvas_image_width, canvas_image_height);

//        pdf = self.addPdfHeader(pdf);
//        pdf = self.addPdfFooter(pdf, (i + 1));
//    }

//    pdf.save(self.documentSlug + ".pdf");

//    self.resetDocumentAfterPrint();
//});
//window.jsPDF = window.jspdf.jsPDF;
//var doc = new jsPDF();
//var specialElementHandlers = {
//    '#editor': function (element, renderer) {
//        return true;
//    }
//};

$('#cmd').click(function () {
    debugger;
    window.jsPDF = window.jspdf.jsPDF;
    //doc.fromHTML($('#main-content').html(), 15, 15, {
    //    'width': 170,
    //    'elementHandlers': specialElementHandlers
    //});
    //doc.save('sample-file.pdf');
    var doc = new jsPDF();

    // Source HTMLElement or a string containing HTML.
    var elementHTML = document.querySelector("#main-content");

    doc.html(elementHTML, {
        callback: function (doc) {
            // Save the PDF
            doc.save('document-html.pdf');
        },
        margin: [10, 10, 10, 10],
        autoPaging: 'text',
        x: 0,
        y: 0,
        width: 190, //target width in the PDF document
        windowWidth: 675 //window width in CSS pixels
    });
});

$(document).ready(() => {
    $("#html").val($("#main-content").html());
});

const documentSlug = "some Slug";
const documentTitle = "Some Title";

function genPDF() {
    let html2pdf = new pdfView();
    html2pdf.generate();
}
//assuming jquery is in use

let pdfView = function () {
    //assuming documentSlug is a constant set earlier 
    this.documentSlug = documentSlug;
    //assuming documentTitle is a constant set earlier
    this.documentTitle = documentTitle;

    //in this html node the part which shall be converted to pdf
    this.nodeId = "main-content";
};

pdfView.prototype.generate = function () {
    let self = this;
    this.prepareDocumentToPrint();

    //variables
    let HTML_Width = $("#" + self.nodeId).width();
    let HTML_Height = $("#" + self.nodeId).height();
    let top_left_margin = 15;
    let PDF_Width = HTML_Width + (top_left_margin * 2);
    let PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);

    this.pdfWidth = PDF_Width;
    this.pdfHeight = PDF_Height;

    let canvas_image_width = HTML_Width;
    let canvas_image_height = HTML_Height;

    let totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

    html2canvas($("#" + self.nodeId)[0], { allowTaint: true, scale: 2, useCORS: true }).then(function (canvas) {
        canvas.getContext('2d');

        //console.log(canvas.height+"  "+canvas.width);

        let pdf = new jsPDF('p', 'mm', [PDF_Width, PDF_Height]);
        let imgData = canvas.toDataURL('image/jpeg', 1.0);
        //let imgData = canvas.toDataURL();
        pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);

        pdf = self.addPdfHeader(pdf);
        pdf = self.addPdfFooter(pdf, 1);

        for (let i = 1; i <= totalPDFPages; i++) {
            pdf.addPage(PDF_Width, PDF_Height);

            pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4) + top_left_margin, canvas_image_width, canvas_image_height);

            pdf = self.addPdfHeader(pdf);
            pdf = self.addPdfFooter(pdf, (i + 1));
        }

        pdf.save(self.documentSlug + ".pdf");

        self.resetDocumentAfterPrint();

    });
};

//this one sets a fixed viewport, to be able to 
//get the same pdf also on a mobile device. I also
//have a css file, which holds the rules for the 
//document if the body has the .printview class added
pdfView.prototype.prepareDocumentToPrint = function () {
    let viewport = document.querySelector("meta[name=viewport]");
    viewport.setAttribute('content', 'width=1280, initial-scale=0.1');
    $('body').addClass("printview");
    window.scrollTo(0, 0);
};

pdfView.prototype.addPdfHeader = function (pdf) {

    pdf.setFillColor(255, 255, 255);
    pdf.rect(0, 0, this.pdfWidth, 40, "F");

    pdf.setFontSize(40);
    pdf.text("Document: " + this.documentTitle + " - Example Lmtd. Inc. (Whatsoever)", 50, 22);

    return pdf;
};

pdfView.prototype.addPdfFooter = function (pdf, pageNumber) {

    pdf.setFillColor(255, 255, 255);
    pdf.rect(0, pdf.internal.pageSize.height - 40, this.pdfWidth, this.pdfHeight, "F");

    pdf.setFontSize(40);
    pdf.text("Seite " + pageNumber, 50, pdf.internal.pageSize.height - 20);
    return pdf;
};

//and this one resets the doc back to normal
pdfView.prototype.resetDocumentAfterPrint = function () {
    $('body').removeClass("printview");
    let viewport = document.querySelector("meta[name=viewport]");
    viewport.setAttribute('content', 'device-width, initial-scale=1, shrink-to-fit=no');
};
$(() => {
    $(".table").DataTable({
        "processing": true,
        //"serverSide": true,
        pageLength: 20,
        'info': true,
        lengthMenu: [[10, 20, 50, 100, 150, -1], [10, 20, 50, 100, 150, 'All']],
        "aaSorting": [], // initial column sorting
        //language: {
        //    oPaginate: {
        //        sNext: '<i class="fa fa-forward"></i>',
        //        sPrevious: '<i class="fa fa-backward"></i>',
        //        sFirst: '<i class="fa fa-step-backward"></i>',
        //        sLast: '<i class="fa fa-step-forward"></i>'
        //    }
        //}
        //pagingType: "full" //Hide page number
        "columnDefs": [{
            "targets": [0,-1],// disable sorting columns
            "searchable": false, "orderable": false, "visible": true
        }],
        aoColumnDefs: [
            {
                bSortable: false,
                aTargets: [-1]
            }
        ]
        //order: [[2, 'asc']]//Order by
    });
});