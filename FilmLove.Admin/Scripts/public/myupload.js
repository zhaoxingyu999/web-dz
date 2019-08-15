var myupload = {
    bindImgUpload: function (file_controlid, img_id, orghd_id, ftype) {
        $('#' + file_controlid).parent().dmUploader({
            url: '/Tool/UploadFile',
            dataType: 'json',
            extraData: { fileType: ftype },
            allowedTypes: 'image/*',
            onUploadSuccess: function (id, data) {
                $('#' + img_id).attr('src', data.urlpath)
                $('#' + orghd_id).val(data.urlpath).change();
            },
            onUploadError: function (id, message) {
                msg.warn("上传失败！" + message)
            }
        });
    },
    bindImgUpload2: function (file_controlid, ftype, callback) {
        $('#' + file_controlid).parent().dmUploader({
            url: '/Tool/UploadFile',
            dataType: 'json',
            extraData: { fileType: ftype },
            allowedTypes: 'image/*',
            onUploadSuccess: function (id, r) {
                if (r.code != 0) {
                    msg.warn(r.msg);
                    return;
                }
                callback(r.data);
            },
            onUploadError: function (id, message) {
                msg.warn("上传失败！" + message)
            }
        });
    }
};