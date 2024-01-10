//var arrIdStatusPair = [];
//var arrStudnetIds = [];


//var RIBBONCONTROLTEACHER = {
//    DoActiveInActiveStudent: function (bActive) {
//        debugger
//        var url = '';
//        url = "Teacher/DoActiveInActiveStudent";
//        $.ajax({
//            url: url,
//            cache: false,
//            type: "POST",
//            dataType: "json",
//            data: {
//                arrStudnetIds: arrStudnetIds,
//                bActive: bActive
//            },
//            success: function (data) {
//                COMMONMETHODS.SaveStatus(true, data);
//            }
//        });
//    },

//    GetStudentIds: function (ele, iStudentId) {
//        debugger;
//        if (ele.checked) {
//            arrStudnetIds.push(iStudentId);
//        } else {
//            for (var i = 0; i < arrStudnetIds.length; i++) {
//                if (iStudentId == arrStudnetIds[i])
//                    break;
//            }
//            arrStudnetIds.splice(i, 1);
//        }
//    },
//}