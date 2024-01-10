

var ACCOUNTCOMMONJS = {
    Test: function () {
        debugger
        alert("Calling test from ACCOUNTCOMMONJS.js file");
    },



    SelectRowFeeStructure: function (eleTr, tbodyId) {
        debugger;
        iFeeStructureId = parseInt(eleTr.id);
        RIBBONCONTROLTEACHER.ResetRowColor(tbodyId);
        eleTr.className = 'SelectRow';
    },
}