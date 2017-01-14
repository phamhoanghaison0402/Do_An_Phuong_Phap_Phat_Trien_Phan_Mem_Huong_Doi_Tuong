vnTProduct = {
	
	check_all:function()	
	{		
		var c = $("#all").attr('checked');
		
		$("#List_Product").find('input:checkbox' ).attr( 'checked', function() {
			var item_id = 'item'+$(this).val();
			if (c){
				$('#'+item_id).addClass('item_select')	;
				return 'checked';
			}else{
				$('#'+item_id).removeClass('item_select')	;	
				return '';	
			}
		}); 
				
	},
	
	select_item:function(id)	
	{
		var item_id = 'item'+id;
		var c = $("#"+item_id+" #ch_id").attr('checked');
		if (c){
			$('#'+item_id).addClass('item_select')	;
		}else{
			$('#'+item_id).removeClass('item_select')	;	
		}
		  
	},	
		
	FlyItem:function (IDcontrolFly,left, height, opacity, maxwidth,quantity) {
    var IMG = $("#" + IDcontrolFly + " img");
    $("body #ImgSC").remove();
    var tt = IMG.attr("src");
    $("body").append("<img id=\"ImgSC\" style=\"position:fixed; z-index:999; filter:alpha(opacity=" + opacity * 100 + " ); opacity:" + opacity + "; max-width:" + maxwidth + "px ; left:" + left + "px; top:" + height + "px \"; src=\"" + IMG.attr("src") + "\"/>");
    left += 20;
    height = MheightI - ((left * MheightI) / MwidthS) + 20;
    opacity -= 0.02;
    maxwidth -= 5;
    if (left < MwidthS) {
        var timer = setTimeout("vnTProduct.FlyItem('" + IDcontrolFly + "'," + left + "," + height + "," + opacity + "," + maxwidth + "," + quantity + ")", 20);
    }
    else {
       $("body #ImgSC").remove();        
			 $("#ext_numcart").text(parseInt($("#ext_numcart").text()) + quantity);
    }
	},
	
	//do_AddItemFlyCart 
	do_AddItemFlyCart:function (idControlS,id) {
 		
		var quantity = $("#quantity").val();  
		if(quantity =='undefined') quantity = 1;		
		quantity = parseInt(quantity);
					
 	  var mydata =  'id='+ id+'&quantity='+quantity;  
		
		$.ajax({
			async: true,
			dataType: 'json',
			url: ROOT+"modules/product/ajax/ajax.php?do=add_cart",
			type: 'POST',
			data: mydata ,
			success: function (data) {
				if(data.ok == 1)	{
					 $(document).scrollTop(0); 
					MwidthS = $("#ext_numcart").offset().left;
					MheightS = $("#ext_numcart").offset().top;
					if (typeof ($("#" + idControlS + " img")) != 'undefined') {
							MwidthI = $("#" + idControlS + " img").offset().left;
							MheightI = $("#" + idControlS + " img").offset().top;
					}										  
					vnTProduct.FlyItem(idControlS, MwidthI, MheightI, 1, 300,quantity); 						
				}	else {
					jAlert(data.mess,'Báo lỗi');
				}	 
 				
			}
		})
	},
	 
	
	
	DoAddCart:function (){ 
		var aList = new Array;
		var count=0;
		$("#List_Product :input:checkbox").each( function() {
				if( $(this).attr('checked') && $(this).attr('id')!="all" ){
					aList.push($(this).val());
					count=count+1;
				} 																																 
		} );		
		
 		if (count==0){ jAlert('Vui lòng chọn 1 sản phẩm', 'Báo lỗi') }
 		else {
			p_id = aList.join(',');	
			location.href=ROOT+'san-pham/cart.html/?do=add&pID='+p_id;	
		}
	  
		return false;
	
	},
	
	DoCompare:function (){
		var catRoot =  $("#catRoot").val();
		var aList = new Array;
		var count=0;
		$("#List_Product :input:checkbox").each( function() {
				if( $(this).attr('checked') && $(this).attr('id')!="all" ){
					aList.push($(this).val());
					count=count+1;
				} 																																 
		} );
		
		
		if(catRoot =='') { jAlert('Không thể so sánh do không cùng chủng loại', 'Báo lỗi')}
		else if (count==0){ jAlert('Vui lòng chọn 1 sản phẩm để so sánh', 'Báo lỗi') }
		else if (count>3){ jAlert('Vui lòng chọn tối đa 3 sản phẩm để so sánh', 'Báo lỗi') }
		else {
			p_id = aList.join(',');	
			location.href=ROOT+'san-pham/compare_product.html/?catRoot='+catRoot+'&p_id='+p_id;	
		}
	  
		return false;
	
	},

	do_WishList:function (doAction,id) {
 
 		if(mem_id>0)
		{
			var mydata =  "act="+doAction +'&id='+id; 
			$.ajax({
				async: true,
				dataType: 'json',
				url: ROOT+"modules/product/ajax/ajax.php?do=wishList",
				type: 'POST',
				data: mydata ,
				success: function (data) {
					jAlert(data.mess, 'Thông báo');					    
				}
			})
		
		}else{
			jAlert('Vui lòng đăng nhập thành viên','Báo lỗi', function() {	vnTRUST.customer.login.show(); 	});  
		}
		return false ;
	} 
	 
	
};
  
vnTCart = {
	
	changeCountry:function(country,lang,selname)	
	{		
		var mydata =  "country="+country +'&selname='+selname+'&lang='+lang; 
		$.ajax({
			async: true,
			dataType: 'json',
			url: ROOT+"modules/product/ajax/ajax.php?do=list_city",
			type: 'POST',
			data: mydata ,
			success: function (data) {
				$("#ext_"+selname).html(data.html) ;
			}
		})				
	} 
	 
	
};

function format_number (num) {
	num = num.toString().replace(/\$|\,/g,'');
	if(isNaN(num))
	num = "0";
	sign = (num == (num = Math.abs(num)));
	num = Math.round(num*100+0.50000000001);
	num = Math.round(num/100).toString();
	for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
	num = num.substring(0,num.length-(4*i+3))+','+ num.substring(num.length-(4*i+3));
	return (((sign)?'':'-') + num);
}

 
function load_image(src,src_big)
{
	/*$("#divImage img").fadeTo('slow', 0.1, function() {
		$(this).attr( "src", src ).animate({ opacity: 1.0 }, 500) ;
	});*/
	$("#divImage img").attr( "src", src );
	
	$("#divImage a").attr( "href", src_big ) ;
}  

function numberFormat(nStr){
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1))
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	return x1 + x2;
}

function formatCurrency(div_id,str_number){
	/*Convert tu 1000->1.000*/
	/*var mynumber=1000;str_number = str_number.replace(/\./g,"");*/
	document.getElementById(div_id).innerHTML = '<font color=blue>' + numberFormat(str_number) + '<font>'; 
	document.getElementById(div_id).innerHTML = document.getElementById(div_id).innerHTML + ' <font color=red>VND</font>';
}


function scrollToBooking(id_scroll) {
	jQuery('html, body').animate({scrollTop: jQuery("#"+id_scroll).offset().top}, 500);
	
}

function view_picture_other ()
{
	$('.picDetail .ad-image a:first-child').click(); 	
}

function closeAdvFilter() {
  $(".showfeature").slideToggle();
}


function resize_control(){
  var leftOffset = jQuery('.name-sp').offset().left;
  jQuery('.item-control').css({'left': (leftOffset - 82) + 'px', 'display': 'block'});
  bodyWidth = document.body.clientWidth;
  jQuery(window).scroll(function() {
    if(jQuery(window).scrollTop() >= 0 && jQuery(window).scrollTop() < 530 && bodyWidth > 768){
      jQuery('.item-control').css({'position':'fixed', 'top': (530 - jQuery(window).scrollTop()) + 'px','left': (leftOffset - 82) + 'px', 'z-index':'50'});
    }else{
      jQuery('.item-control').css({'position':'fixed', 'top': '45px','left': (leftOffset - 82) + 'px', 'z-index':'50'});
    }
  });
}


function formatMoneyVND(money) {
	if (money == 0) {
		return 0;
	}
	var tmp = money.toString().split('').reverse().join('');
	var a = [];
	var len = tmp.length;
	var b = true;
	while (b) {
		if (tmp.indexOf(",") > 0) {
			tmp = tmp.replace(",", "");
			b = true;
		}
		else {
			b = false;
		}
	}
	b = true;
	while (b) {
		len = tmp.length;
		if (len % 3 != 0) {
			tmp = tmp.toString() + '0';
			b = true;
		}
		else {
			b = false;
		}
	}
	for (var i = 0; i < tmp.length; i += 3) {
		a.push(tmp[i] + tmp[i + 1] + tmp[i + 2]);
	}
	tmp = a.toString().split('').reverse().join('');
	b = true;
	while (b) {
		if (tmp[0] == 0 || tmp[0] == ',') {
			tmp = tmp.substr(1);
			b = true;
		}
		else {
			b = false;
		}
	}
	return tmp;
}

jQuery(window).bind("resize", resize_control);

$(document).ready(function() {


  jQuery('#cp-diem-noi-bat').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#diem-noi-bat").offset().top-40}, 300);
  });
  jQuery('#cp-thong-so').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#parameter").offset().top-40}, 300);
  });
  jQuery('#cp-anh-thuc-te').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#video_picture").offset().top-40}, 300);
  });
  jQuery('#cp-video-thuc-te').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#video_picture").offset().top-40}, 300);
  });
  jQuery('#cp-danh-gia').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#danh-gia").offset().top-40}, 300);
  });
  jQuery('#cp-binh-luan').click(function(){
    jQuery('html, body').animate({scrollTop: jQuery("#binh-luan").offset().top-40}, 300);
  });




  $('.btn_show_price').click(function() {

    var btn = $(this);
    var price = $('#price');
    var price_2 = $('#price_2');

    if(btn.hasClass("clicked"))
    {
      price.hide();
      price_2.show();
      btn.removeClass("clicked").html("Khách hàng <strong>Hồ Chí Minh</strong> xem giá tại đây  ›")
    }else{
      price.show();
      price_2.hide();
      btn.addClass("clicked").html("Khách hàng <strong>Đà Nẵng</strong> xem giá tại đây  ›")
    }
  });


  $('.submit-your-review').click(function(){
    //get collapse content selector
      var collapse_content_selector = $(this).attr('href');
      //make the collapse content to be shown or hide
      var toggle_switch = $(this);
      $(collapse_content_selector).slideToggle(function(){
        if($(this).css('display')=='none'){
          //change the button label to be 'Show'
          toggle_switch.html('Viết nhận xét của bạn');
        }else{
          //change the button label to be 'Hide'
          toggle_switch.html('Đóng lại');
        }
      });

  });


  $(".moresearch").on("click", function() {
    $(".showfeature").slideToggle();
  });

  $("#view_address").on("click", function() {
    $(".address_company").slideToggle();
  });

  //danh gia
  jQuery('.close-evaluation').click(function(){
    jQuery('.evaluation-detail').hide();
    jQuery('.evaluation-brief').show();
  });

  jQuery('.evaluation-detail').hide();
  jQuery('.more-evaluation').click(function(){
    jQuery('.evaluation-brief').hide();
    jQuery('.evaluation-detail').show();
    jQuery('.item-control li').removeClass('item-control-active');
    jQuery('#cp-danh-gia').addClass('item-control-active');
    jQuery('html, body').animate({scrollTop: jQuery('#danh-gia').offset().top}, 300);
  });



  //load color

  $('ul.color-list-show li').click(function(){
    jQuery('ul.color-list-show li').removeClass('color-active');
    jQuery(this).addClass('color-active');

    //load color and price
    var id = $("#p_id").val();
    var str = this.id;
    var color_id = str.replace("color_id_","");
    var mydata =  "id="+id+"&color_id="+color_id;

    $.ajax({
      async: true,
      dataType: 'json',
      url: ROOT+"modules/product/ajax/ajax.php?do=load_picture_color",
      type: 'POST',
      data: mydata ,
      success: function (data) {
        if(data.ok){
					var $fotoramaDiv = $('#albumDetail').fotorama();
					var fotorama = $fotoramaDiv.data('fotorama');

					//console.log(fotorama);
					var list_image = data.list_pic;
					var str= list_image.replace(/"img"/g, 'img');
					var str_ = eval(str);
					fotorama.load(str_);

          $("#color_id").val(color_id);
					$('[name=color_id]').change();
					$('.input_color .glyphicon').hide();
        }

        if(data.ok_price){
          $(".price").html(data.price);
        }

      }
    })

  });





});



