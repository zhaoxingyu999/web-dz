/* ******************************************
*	初始化SWFUpload上传控件
* ****************************************** */
function InitSWFUpload(uploadid,type) {
    var sendUrl = "/File/UploadImage";
    var swfu = new SWFUpload({
        // Backend Settings
        upload_url: sendUrl,
        file_post_name: "",
        post_params: {
            "ASPSESSID": "NONE"
            , "TYPE": type
        },

        file_size_limit: "2MB", // 2MB
        file_types: "*.jpg;*.jpge;*.png;*.gif",
        file_types_description: "JPG Images",
        file_upload_limit: "0",
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: uploadid,
        button_width: 48,
        button_height: 21,
        button_text: '浏览...',
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: "/Scripts/swfupload/swfupload.swf",
        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });
}

/* ******************************************
*	返回上传的状态
* ****************************************** */
function uploadSuccess(file, serverData) {
    try {
        var progress = new FileProgress(file, this.customSettings.upload_target);
    var jsonstr = eval('(' + serverData + ')');
        if (jsonstr.status) {
            addImage(jsonstr.path, jsonstr.code);
        }
        else {
            progress.setStatus(jsonstr.msgbox);
        }
        progress.toggleCancel(false);
        
    } catch (ex) {
        this.debug(ex);
    }
}

/*******************************************
*   添加图片列表
********************************************/
function addImage(path,code) {
    var picCount = $("#tb_pics tr").length;
    var fullUrl = path;
    var html = "<tr id=\"tr_" + code + "\">";
    html += "<td style=\"text-align:center;\"><img width=\"50\" src=\"" + fullUrl + "\" />";
    var isdefault = 0;
    if (picCount == 0) {
        isdefault = 1;
    }
    html += "<span id=\"spdata_" + code + "\" class=\"imgdata\" data-url=\"" + fullUrl + "\" data-path=\"" + path + "\" data-isdefault=\"" + isdefault + "\" data-code=\"" + code + "\"></span>";
    html += "</td>";
    html += "<td style=\"text-align:center;\"><a href=\"" + fullUrl + "\" target=\"_blank\">查看</a></td>";
    if (picCount == 0) {
        html += "<td style=\"text-align:center;\"><span id=\"sp_" + code + "\" class=\"red clr\">是</span></td>";
    }
    else {
        html += "<td style=\"text-align:center;\"><span id=\"sp_" + code + "\" class=\"green clr\">否</span></td>";
    }
    html += "<td style=\"text-align:center;\"><a href=\"javascript:void(0);\" class=\"setdefault\" onclick=\"prepro.setisdefault('" + code + "');\">设为默认图</a>";
    html += "<a href=\"javascript:void(0);\" class=\"setdelete\" onclick=\"prepro.setdelete('" + code + "');\">删除</a></td>";
    html += "</tr>";
    $("#tb_pics").append(html);
}

/* ******************************************
*	删除LI元素
* ****************************************** */
function del_img(obj) {
    var focusphoto = $("#focus_photo");
    var smallimg = $(obj).prevAll(".img_box").children("img").eq(0).attr("src");
    var node = $(obj).parent(); //要删除的LI节点
    node.remove(); //删除DOM元素
    //检查是否为封面
    if (focusphoto.val() == smallimg) {
        focusphoto.val("");
        var firtimg = $("#show_list ul li .img_box img").eq(0);
        firtimg.parent().addClass("current"); //取第一张做为封面
        focusphoto.val(firtimg.attr("src")); //重新给封面的隐藏域赋值
    }
}

/* ******************************************
*	设置相册封面
* ****************************************** */
function focus_img(obj) {
    $("#focus_photo").val($(obj).children("img").eq(0).attr("src"));
    $("#show_list ul li .img_box").removeClass("current");
    $(obj).addClass("current");
}

/* ******************************************
*	显示图片链接
* ****************************************** */
function show_remark(obj) {
    //取得隐藏值
    var hidRemark = $(obj).prevAll("input[name='hide_photo_remark']").eq(0);
    var m = $.ligerDialog.open({
        type: "",
        title: "图片描述",
        content: '<textarea id="ImageRemark" style="font-size:12px;padding:3px;color:#000;border:1px #d2d2d2 solid;vertical-align:middle;width:300px;height:50px;">' + $(hidRemark).val() + '</textarea>',
        width: 350,
        buttons: [
        { text: '批量描述', onclick: function () {
            var remark = $('#ImageRemark').val();
            if (remark == "") {
                $.ligerDialog.error('总该写点什么吧？');
                return false;
            }
            $("input[name='hide_photo_remark']").val(remark);
            $(".img_box").find(".remark i").html(remark);
            m.close();
        }
        },
        { text: '单张描述', onclick: function () {
            var remark = $('#ImageRemark').val();
            if (remark == "") {
                $.ligerDialog.error('总该写点什么吧？');
                return false;
            }
            $(hidRemark).val(remark);
            $(obj).prevAll(".img_box").find(".remark i").html(remark);
            m.close();
        }
        }
        ],
        isResize: true
    });
}

function fileQueueError(file, errorCode, message) {
	//try {
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setError();
		progress.toggleCancel(false);
		if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
			errorName = "您选择的文件太多.";
		}
		if (errorName !== "") {
			alert(errorName);
			return;
		}

		switch (errorCode) {
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
			progress.setStatus(file.name+"文件太小");
			progress.toggleCancel(false, this);
			break;
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
			progress.setStatus(file.name+"文件太大");
			progress.toggleCancel(false, this);
			break;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
			progress.setStatus(file.name+"文件类型出错");
			progress.toggleCancel(false, this);
			break;
		default:
			if (file !== null) {
				progress.setStatus("未知错误");
				progress.toggleCancel(false, this);
			}
			break;
		}

	//} catch (ex) {
	//	this.debug(ex);
	//}

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
	//try {
		if (numFilesQueued > 0) {
			this.startUpload();
		}
	//} catch (ex) {
	//	this.debug(ex);
	//}
}

function uploadProgress(file, bytesLoaded) {

	//try {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);
        var progress = new FileProgress(file,  this.customSettings.upload_target);
		progress.setProgress(percent);
		if (percent === 100) {
			progress.setStatus("完成");
			progress.toggleCancel(false, this);
		} else {
			progress.setStatus("上传中...");
			progress.toggleCancel(true, this);
		}
	//} catch (ex) {
	//	this.debug(ex);
	//}
}

function uploadComplete(file) {
	//try {
		/*  I want the next upload to continue automatically so I'll call startUpload here */
		if (this.getStats().files_queued > 0) {
			this.startUpload();
		} else {
			var progress = new FileProgress(file,  this.customSettings.upload_target);
			progress.setComplete();
			progress.setStatus("上传完成.");
			progress.toggleCancel(false);
		}
		$("#" + this.customSettings.upload_target).html("");
	//} catch (ex) {
	//	this.debug(ex);
	//}
}

function uploadError(file, errorCode, message) {
	var imageName =  "error.gif";
	var progress;
	//try {
		switch (errorCode) {
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
			try {
				progress = new FileProgress(file,  this.customSettings.upload_target);
				progress.setCancelled();
				progress.setStatus("Cancelled");
				progress.toggleCancel(false);
			}
			catch (ex1) {
				this.debug(ex1);
			}
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
			try {
				progress = new FileProgress(file,  this.customSettings.upload_target);
				progress.setCancelled();
				progress.setStatus("Stopped");
				progress.toggleCancel(true);
			}
			catch (ex2) {
				this.debug(ex2);
			}
		case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
			imageName = "uploadlimit.gif";
			break;
		default:
			alert(message);
			break;
		}
		addImage("images/" + imageName);
	//} catch (ex3) {
	//	this.debug(ex3);
	//}
}

function fadeIn(element, opacity) {
	var reduceOpacityBy = 5;
	var rate = 30;	// 15 fps


	if (opacity < 100) {
		opacity += reduceOpacityBy;
		if (opacity > 100) {
			opacity = 100;
		}

		if (element.filters) {
			try {
				element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
			}
		} else {
			element.style.opacity = opacity / 100;
		}
	}

	if (opacity < 100) {
		setTimeout(function () {
			fadeIn(element, opacity);
		}, rate);
	}
}

/* ******************************************
 *	FileProgress Object
 *	Control object for displaying file info
 * ****************************************** */

function FileProgress(file, targetID) {
	this.fileProgressID = "divFileProgress";

	this.fileProgressWrapper = document.getElementById(this.fileProgressID);
	if (!this.fileProgressWrapper) {
		this.fileProgressWrapper = document.createElement("div");
		this.fileProgressWrapper.className = "progressWrapper";
		this.fileProgressWrapper.id = this.fileProgressID;

		this.fileProgressElement = document.createElement("div");
		this.fileProgressElement.className = "progressContainer";

		var progressCancel = document.createElement("a");
		progressCancel.className = "progressCancel";
		progressCancel.href = "#";
		progressCancel.style.visibility = "hidden";
		progressCancel.appendChild(document.createTextNode(" "));

		var progressText = document.createElement("div");
		progressText.className = "progressName";
		progressText.appendChild(document.createTextNode(file.name));

		var progressBar = document.createElement("div");
		progressBar.className = "progressBarInProgress";

		var progressStatus = document.createElement("div");
		progressStatus.className = "progressBarStatus";
		progressStatus.innerHTML = "&nbsp;";

		this.fileProgressElement.appendChild(progressCancel);
		this.fileProgressElement.appendChild(progressText);
		this.fileProgressElement.appendChild(progressStatus);
		this.fileProgressElement.appendChild(progressBar);

		this.fileProgressWrapper.appendChild(this.fileProgressElement);

		document.getElementById(targetID).appendChild(this.fileProgressWrapper);
		fadeIn(this.fileProgressWrapper, 0);

	} else {
		this.fileProgressElement = this.fileProgressWrapper.firstChild;
		this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
	}

	this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
	this.fileProgressElement.className = "progressContainer green";
	this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
	this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
	this.fileProgressElement.className = "progressContainer blue";
	this.fileProgressElement.childNodes[3].className = "progressBarComplete";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setError = function () {
	this.fileProgressElement.className = "progressContainer red";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setCancelled = function () {
	this.fileProgressElement.className = "progressContainer";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setStatus = function (status) {
	this.fileProgressElement.childNodes[2].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
	this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
	if (swfuploadInstance) {
		var fileID = this.fileProgressID;
		this.fileProgressElement.childNodes[0].onclick = function () {
			swfuploadInstance.cancelUpload(fileID);
			return false;
		};
	}
};
