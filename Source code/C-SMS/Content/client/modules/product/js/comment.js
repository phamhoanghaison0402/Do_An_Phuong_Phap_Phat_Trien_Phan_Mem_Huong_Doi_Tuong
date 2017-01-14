var vnTcomment = {   
	
	/*  show_comment */
	show_comment:function(id,lang,p) {
		$.ajax({
			 type:"POST",
			 url: ROOT+'modules/product/ajax/comment.php?do=list',
			 data: "id="+id+'&lang='+lang+'&p='+p,
			 success: function(html){
					$("#ext_comment").html(html);
			 }
		 });
	},

  show_comment_focus:function(id,lang,p) {
    $.ajax({
      type:"POST",
      url: ROOT+'modules/product/ajax/comment.php?do=list_focus',
      data: "id="+id+'&lang='+lang+'&p='+p,
      success: function(html){
        $("#ext_comment_focus").html(html);
      }
    });
  },


  select_vote:function(num) {
		var uservote=num;
		for (i=1;i<=5;i++) {
			objname='vote_'+i;
			var imgshow=(i<=num) ? 'star2.gif':'star0.gif';
			src =  ROOT+"modules/product/images/"+imgshow;
			$("#"+objname).attr("src", src); 
		}	
		
		$('#hvote').val(num);
	},


	/*  post_comment */
	post_comment:function (id,lang)
	{
		var ok_post = true ;
		var mess_err='';


    var mark = $("#hvote").val();
    if (mark == '' || mark==0 ) {
      jAlert(js_lang['err_mark_empty'],js_lang['error'], function() {	 	});
      ok_post = false ;
      return false;
    }

    var name = $("#name").val()
    if (name == '') {
      jAlert(js_lang['err_name_empty'],js_lang['error'], function() {	$("#name").focus(); 	});
      ok_post = false ;
      return false;
    }

    var email = $("#email").val()
    /*if (email == '') {
      jAlert(js_lang['err_email_empty'],js_lang['error'], function() {	$("#email").focus(); 	});
      ok_post = false ;
      return false;
    }*/


    var content= $("#txtComment").val();
		if ( content.length <10) {
			jAlert('Vui lòng nhập nội dung tối thiểu 10 ký tự','Báo lỗi', function() {	 $("#txtComment").focus(); 	});   
			ok_post = false ;
			return false;
		}
			 


    if(ok_post)
		{			
			name = encodeURIComponent(name);
			content = encodeURIComponent(content);
 			
			var mydata =  "id="+id+"&name="+name+"&email="+email+"&content=" + content+"&mark="+mark+"&lang="+lang ;
			$.ajax({
				async: true,
				dataType: 'json',
				url:  ROOT+"modules/product/ajax/comment.php?do=post" ,
				type: 'POST',
				data: mydata ,
				success: function (data) {
					if(data.ok == 1)	{
						// vnTcomment.show_comment (id,lang,0);
						 jAlert(js_lang['send_comment_success']);
						 $("#name").val('')		;
              $("#email").val('')		;
						 $("#txtComment").val('')		;
            $("#btnSendCmt").show();
            $(".showdropdown").hide();
						 
					}	else {
						jAlert('Có lỗi xảy ra .','Báo lỗi');
					}	   
				}
			}) 		
		} 
		
		return false;
	},
	
 
	/*  post_subcomment */
	post_votes:function (id)
	{
		  
		var rating = $("input[name='rating']:checked").val();
		
		var mydata =  "id="+id+ "&rating=" + rating  ;
		$.ajax({
			async: true,
			dataType: 'json',
			url:  ROOT+"modules/product/ajax/comment.php?do=votes" ,
			type: 'POST',
			data: mydata ,
			success: function (data) {
				if(data.ok == 1)	{
					$("#ext_votes").html('<div class="ajax_mess" >Cám ơn bạn đã tham gia bình chọn</div>');					 
				}	else {
					jAlert('Có lỗi xảy ra .','Báo lỗi');
				}	   
			}
		}) 		
		  
		
		return false;
	},
	
	/******POST SUB COMMENT******/
		/*  post_comment_sub */
		
	post_comment_sub:function (id)
	{
		
		
		var ok_post = true ;
		var mess_err='';

    var name = $("#name_"+id).val()
    if (name == '') {
      jAlert(js_lang['err_name_empty'],js_lang['error'], function() {	$("#name_"+id).focus(); 	});
      ok_post = false ;
      return false;
    }

    var email = $("#email_"+id).val()
    if (email == '') {
      jAlert(js_lang['err_email_empty'],js_lang['error'], function() {	$("#email_"+id).focus(); 	});
      ok_post = false ;
      return false;
    }


		var content= $("#txtComment_"+id).val();		 
		if ( content.length <10) {
			jAlert('Vui lòng nhập nội dung tối thiểu 10 ký tự','Báo lỗi', function() {	 $("#txtComment_"+id).focus(); 	});   
			ok_post = false ;
			return false;
		}
			 

		if(ok_post) 
		{			
			name = encodeURIComponent(name);
			content = encodeURIComponent(content);
 			
			var mydata =  "id="+id+"&name="+name+"&email="+email+"&content=" + content ;
			$.ajax({
				async: true,
				dataType: 'json',
				url:  ROOT+"modules/product/ajax/comment.php?do=postSubComment" ,
				type: 'POST',
				data: mydata ,
				success: function (data) {
					if(data.ok == 1)	{
						 //vnTcomment.show_comment_sub (id,0);
						 jAlert(js_lang['send_comment_success']);
              $("#name_"+id).val('')		;
             $("#email_"+id).val('')		;
						 $("#txtComment_"+id).val('');
						 $('#write_sub_'+id).hide();
						 
					}	else {
						jAlert('Có lỗi xảy ra .','Báo lỗi');
					}	   
				}
			}) 		
		} 
		
		return false;
	},
	
	show_comment_sub:function(id,p) {	
		$.ajax({
			 type:"POST",
			 url: ROOT+'modules/product/ajax/comment.php?do=showSubComment',
			 data: "id="+id+'&p='+p,
			 success: function(html){
					$("#ext_comment_sub_"+id).html(html);
			 }
		 });
	},


	show_post_sub:function (id)
	{
      $("#write_sub_"+id).slideToggle();
		
	},
	
	hide_name:function (lang)
	{
		
		$("#txtComment").val(lang);
		$(".info_hidden").hide();
		
	}
	
}; 



