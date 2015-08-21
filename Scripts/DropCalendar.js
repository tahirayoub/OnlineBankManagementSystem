//Some code and concepts from DateTimePicker.js Copyright (c) 2003 TengYong Ng Website: http://www.rainforestnet.com
//
//DropCalendar.js - Popup Calendar - Date Selector
//Copyright (c) 2007 Daryl V. McMasters 
//Website: http://www.mmprogramming.com
//Version: 1.0
//Contact: darylmc@mmprogramming.com
//NOT be SOLD as standalone script
//May be used free of obligation as part of any larger application as long as 
//these 10 Copyright Info lines are distributed with script.

//Global variables
var dtToday=new Date();
var Cal=null;
var MonthName=["January", "February", "March", "April", "May", "June","July", 
	"August", "September", "October", "November", "December"];
var WeekDayName=["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
var WeekStartsOnDay = 0;	
var exDateTime;//Existing Date and Time
var BodyOverflow="";
var selectControls;
//Configurable parameters
var DefaultFormat="MMDDYYYY";// Default Format to display date.
var cnTop="200";//top coordinate of calendar window.
var cnLeft="500";//left coordinate of calendar window
var cnWidth="150";//width of calendar window - Height is automatic.
var WeekChar=1;//number of character for week day. if 2 then Mo,Tu,We. if 3 then Mon,Tue,Wed.
var CellWidth=25;//Width of day cell.
var DateSeparator="/";//Date Separator, you can change it to "/" if you want.

var MonthYearColor="#cc0033";//Font Color of Month and Year in Calendar header.
var WeekHeadColor="#B7B7FF";//Background Color in Week header.
var SundayColor="#FFFFFF";//Background color of Sunday.
var SaturdayColor="#FFFFFF";//Background color of Saturday.
var WeekDayColor="white";//Background color of weekdays.
var FontColor="blue";//color of font in Calendar day cell.
var TodayColor="#FF0099";//Background color of today.
var SelDateColor="#FFFF99";//Backgrond color of selected date in textbox.
var CalendarBackColor = "#FFFFFF";//Backgrond color of Calendar Window.
var YrSelColor="#cc0033";//color of font of Year selector.
var NavFontSize="2"; //Font size of line of navigation links.
var CalFontSize="2"; //Font size of line of navigation links.
var vDropCalander="";
//end Configurable parameters
//end Global variable


function OpenCal(pCtrl,pFormat)
{
 if( Cal )
 {
  if( Cal.elem )
  {
   var id = Cal.Ctrl.id;
   CloseCal(); //Calendar was open somewhere so close it.
   if( id == pCtrl ) //The user clicked the same control that opened us so and return.
    return;
  }
 } 
 Cal=new Calendar(dtToday);
	if (pCtrl!=null)
	{
		Cal.Ctrl = getCtrl(pCtrl);
		if( !Cal.Ctrl )
		 Cal.Ctrl = {};
		 if( !Cal.Ctrl.id )
		  Cal.Ctrl.id = {};
		Cal.Ctrl.id = pCtrl;
	}
	if( pCtrl==null )
	 return;
	if (pFormat!=null)
		Cal.Format=pFormat.toUpperCase();
	else
		Cal.Format=DefaultFormat.toUpperCase();
 cnLeft=getLeft(Cal.Ctrl);
	
	var exDateTime="";
	exDateTime=Cal.Ctrl.value;
	if (exDateTime!=null && exDateTime!="" && exDateTime!="undefined")//Parse Date String
	{
		var Sp1;//Index of Date Separator 1
		var Sp2;//Index of Date Separator 2 
		var tSp1;//Index of Time Separator 1
		var tSp1;//Index of Time Separator 2
		var strMonth;
		var strDate;
		var strYear;
		var intMonth;
		var YearPattern;
		var strHour;
		var strMinute;
		var strSecond;
		//parse month
		Sp1=exDateTime.indexOf(DateSeparator,0)
		Sp2=exDateTime.indexOf(DateSeparator,(parseInt(Sp1)+1));
		
		if ((Cal.Format.toUpperCase()=="DDMMYYYY") || (Cal.Format.toUpperCase()=="DDMMMYYYY"))
		{
			strMonth=exDateTime.substring(Sp1+1,Sp2);
			strDate=exDateTime.substring(0,Sp1);
		}
		else if ((Cal.Format.toUpperCase()=="MMDDYYYY") || (Cal.Format.toUpperCase()=="MMMDDYYYY"))
		{
			strMonth=exDateTime.substring(0,Sp1);
			strDate=exDateTime.substring(Sp1+1,Sp2);
		}
		if (isNaN(strMonth))
			intMonth=Cal.GetMonthIndex(strMonth);
		else
			intMonth=parseInt(strMonth,10)-1;	
		if ((parseInt(intMonth,10)>=0) && (parseInt(intMonth,10)<12))
			Cal.Month=intMonth;
		//end parse month
		//parse year
		strYear=exDateTime.substring(Sp2+1,Sp2+5);
		YearPattern=/^\d{4}$/;
		if (YearPattern.test(strYear))
			Cal.Year=parseInt(strYear,10);
		//end parse year
		//parse Date
		if ((parseInt(strDate,10)<=Cal.GetMonDays()) && (parseInt(strDate,10)>=1))
			Cal.Date=strDate;
		//end parse Date
	}
	RenderCal();
}

function getCtrl(Elem)
{
 var elem = null;
 if(document.getElementById) 
 {
  elem = document.getElementById(Elem);
 } 
 else if (document.all)
 {
  elem = document.all[Elem];
 }
 return elem;
}

function getTop(Elem) 
{
 var yPos = Elem.offsetTop;
 var tempEl = Elem.offsetParent;
 while (tempEl != null) 
 {
  yPos += tempEl.offsetTop;
  tempEl = tempEl.offsetParent;
 }
 return yPos;
}

function getLeft(Elem) 
{
 xPos = Elem.offsetLeft;
 tempEl = Elem.offsetParent;
 while (tempEl != null) 
 {
  xPos += tempEl.offsetLeft;
  tempEl = tempEl.offsetParent;
 }
 return xPos;
}

function RenderCal()
{
	var vCalHeader;
	var vCalData;
	var i;
	var j;
	var SelectStr;
	var vDayCount=0;
	var vFirstDay;

	vCalHeader="<html><body  bgcolor='#EFDFCF' link="+FontColor+" vlink="+FontColor+"><table id='calT1' border=0 cellpadding=0 cellspacing=0 width='100%' height='100%'><tr><td bgcolor='"+CalendarBackColor+"' bordercolor='#000000' style='border-style: solid; border-width: 1'><table border=0 cellpadding=1 cellspacing=1 width='100%' align='center' valign='top'>";
	//Month Selector
	vCalHeader+="<tr><td colspan='6'><table border=0 width='100%' cellpadding=0 cellspacing=0><tr><td align='left'>";
	//Decrement Year <<
	vCalHeader+="<td align='left' size='10%'><a style='text-decoration:none' href='javascript:DecYear();RenderCal();' title='" + (Cal.Year-1) + "'><b><font size='"+CalFontSize+"' color='"+YrSelColor+"'><<</font></b></a></td>"
	//Decrement Month <
 vCalHeader+="<td align='right' size='5%'><a style='text-decoration:none' href='javascript:DecMonth();RenderCal();' title='" +Cal.GetMonthName(true,-1)+"'><b><font size='"+CalFontSize+"' color='"+YrSelColor+"'><</font></b></a></td>"
	// Month and year being viewed
	vCalHeader+="<td align='center' size='62%'><font size='"+NavFontSize+"' face='Verdana' color='"+MonthYearColor+"'><b>"+Cal.GetMonthName(false)+" " + Cal.Year + "</b></font></td>"
	//Increment Month >
	vCalHeader+="<td align='left' size='5%'><a style='text-decoration:none' href='javascript:IncMonth();RenderCal();' title='" +Cal.GetMonthName(true,1)+ "'><b><font size='"+CalFontSize+"' color='"+YrSelColor+"'>></font></b></a></td>";	
	//Increment Year >>
	vCalHeader+="<td align='right' size='10%'><a style='text-decoration:none' href='javascript:IncYear();RenderCal();' title='" + (Cal.Year+1) + "'><b><font size='"+CalFontSize+"' color='"+YrSelColor+"'>>></font></b></a></td></tr></table></td>";
	//Close for when you change your mind
	vCalHeader+="<td bgcolor='#C0C0C0' bordercolor='#000000' style='border-style: solid; border-width: 1'><center><b><a style='text-decoration:none' href='javascript:CloseCal();' title='Close'><font size='"+NavFontSize+"' color='#800000'>X</font></a></b></center></td>";
	vCalHeader+="</tr>";
	//Week day header
	vCalHeader+="<tr bgcolor="+WeekHeadColor+">";
	for (i=0;i<7;i++)
	{
	 var adj = i + WeekStartsOnDay;
	 if( adj > 6 )
	  adj = adj-7;
		vCalHeader+="<td align='center'><font face='Verdana' size='"+CalFontSize+"'>"+WeekDayName[adj].substr(0,WeekChar)+"</font></td>";
	}
	vCalHeader+="</tr>";	
	
	//Calendar detail
	CalDate=new Date(Cal.Year,Cal.Month);
	CalDate.setDate(1);
	vFirstDay=CalDate.getDay();
	if( vFirstDay < WeekStartsOnDay )
     vFirstDay += (8 - WeekStartsOnDay);
	vCalData="<tr>";
	for (i=WeekStartsOnDay;i<vFirstDay;i++)
	{
		vCalData=vCalData+GenCell();
		vDayCount=vDayCount+1;
	}
	for (j=1;j<=Cal.GetMonDays();j++)
	{
		var strCell;
		vDayCount=vDayCount+1;
		if ((j==dtToday.getDate())&&(Cal.Month==dtToday.getMonth())&&(Cal.Year==dtToday.getFullYear()))
			strCell=GenCell(j,true,TodayColor);//Highlight today's date
		else
		{
			if (j==Cal.Date)
			{
				strCell=GenCell(j,true,SelDateColor);
			}
			else
			{	 
				if (vDayCount%7==0)
					strCell=GenCell(j,false,SaturdayColor);
				else if ((vDayCount+6)%7==0)
					strCell=GenCell(j,false,SundayColor);
				else
					strCell=GenCell(j,null,WeekDayColor);
			}		
		}						
		vCalData=vCalData+strCell;

		if((vDayCount%7==0)&&(j<Cal.GetMonDays()))
		{
			vCalData=vCalData+"</tr><tr>";
		}
	}
	vDropCalander = vCalHeader + vCalData + "</td></tr></table></table></div></body></html>";
 var obj = document.getElementById(Cal.Ctrl.id);	
 var leftpos = getLeft(obj);
 var bottom = getTop(obj) + obj.offsetHeight;
 var Width = getClientWidth();
 var Height = getClientHeight();
 var ScrollPos = getScrollTop();
 var ScrollHeight = document.body.scrollHeight;
 if( Cal.elem==null) 
 { 
  try
  {
   Cal.elem = document.createElement('<div id="cal" style="position:absolute; top:'+ bottom + 'px; left:' + leftpos + 'px; z-index:1; width:' + cnWidth + 'px; height:0px;"></div>');
  }
  catch(err)
  {
   Cal.elem = document.createElement("div");
   Cal.elem.setAttribute('type','file');
   Cal.elem.setAttribute('id','cal');
   Cal.elem.setAttribute("style","position:absolute; top:"+bottom+"; left:"+leftpos+"; z-index:1; width:" + cnWidth + "px; height:0px;");
  }
 }
 Cal.elem.innerHTML = vDropCalander;
 BodyOverflow = document.body.style.overflow;
 //alert(ScrollHeight);
 if( BodyOverflow == 'auto' && ScrollHeight <= Height ) // if height of page is close to browser client height
  document.body.style.overflow = 'hidden';              // and overflow is auto, the addition of the div could
 document.body.appendChild(Cal.elem);                   // make the scrollbar show so lets hide it while the
 var elemHeight = Cal.elem.offsetHeight;                // calendar is up
 if( !elemHeight )
  elemHeight = document.getElementById('calT1').offsetHeight;
 if( (Cal.elem.offsetTop + elemHeight) > (Height + ScrollPos) )
 {
 //alert((Cal.elem.offsetTop + elemHeight) + " - " + Height + " - " + ScrollPos);
  if( Cal.elem.style.top )
   Cal.elem.style.top = (getTop(obj) - elemHeight) + "px"; 
  else 
   Cal.elem.setAttribute("style.top",(getTop(obj) - Cal.elem.offsetHeight) + "px"); 
 }
 while( Cal.elem.offsetLeft + Cal.elem.offsetWidth > (Width) )
  if( Cal.elem.style.left )
   Cal.elem.style.left = (getLeft(Cal.elem) - 1) + "px";
  else
   Cal.elem.setAttribute("style.left",(getLeft(Cal.elem) - 1) + "px");

 var oSelects=document.getElementsByTagName("select");
 selectControls = new Array(oSelects.length);
 hideSelects();
}
	
function CloseCal() 
{
 document.body.removeChild(Cal.elem);
 document.body.style.overflow = BodyOverflow;
 
 delete Cal;
 Cal = null;
 delete selectControls;
 showSelects();
}

function CalClicked(PValue) 
{
 Cal.Ctrl.value = Cal.FormatDate(PValue);
 CloseCal();
}

function GenCell(pValue,pHighLight,pColor)//Generate table cell with value
{
	var PValue;
	var PCellStr;
	var vColor;
	var vHLstr1;//HighLight string
	var vHlstr2;
	
	if (pValue==null)
		PValue="";
	else
		PValue=pValue;
	
	if (pColor!=null)
		vColor="bgcolor='"+pColor+"'";
	else
		vColor="";	
	if ((pHighLight!=null)&&(pHighLight))
		{vHLstr1="color='"+pHighLight+"'><b>";vHLstr2="</b>";}
	else
		{vHLstr1=">";vHLstr2="";}
		var dq = '"';	
	PCellStr="<td "+vColor+" width="+dq+CellWidth+dq+" align="+dq+"center"+dq+"><a style="+dq+"text-decoration:none"+dq+" href="+dq+"javascript:CalClicked('"+PValue+"');"+dq+"><font face="+dq+"Verdana"+dq+" size="+dq+CalFontSize+dq+" "+vHLstr1+PValue+"</font></a></td>";
	return PCellStr;
}

function getClientWidth() 
{
 	return getResults (
		window.innerWidth ? window.innerWidth : 0,
		document.documentElement ? document.documentElement.clientWidth : 0,
		document.body ? document.body.clientWidth : 0
	);
}

function getScrollTop() 
{
	return getResults (
		window.pageYOffset ? window.pageYOffset : 0,
		document.documentElement ? document.documentElement.scrollTop : 0,
		document.body ? document.body.scrollTop : 0
	);
}

function getClientHeight() 
{
	return getResults (
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}

function getResults(n_win, n_docel, n_body) 
{
	var n_result = n_win ? n_win : 0;
	if (n_docel && (!n_result || (n_result < n_docel)))
		n_result = n_docel;
	return n_body && (!n_result || (n_result < n_body)) ? n_body : n_result;
}

fDomOffset = function( oObj, sProp )
{ 
 var iVal = 0; 
 while (oObj && oObj.tagName != 'BODY') 
 { 
  eval('iVal += oObj.' + sProp + ';');
  oObj = oObj.offsetParent; 
 } 
 return iVal; 
} 

function showSelects()
{
 if( !selectControls[0] ) return;
 var i;
 for(var i=0; i < selectControls.length && selectControls[i]; i++ )
  selectControls[i].style.visibility="visible";
}

function hideSelects()
{
 if(!document.all )
 {
  selectControls[0] = null;
  return; //not IE
 }
 var b_version=navigator.appVersion  // start testing for IE version 7 or higher
 var verels = b_version.split(';');
 for( var i = 0; i < verels.length; i++ )
  if( verels[i].indexOf("MSIE") != -1 )
   break;
 if( i != verels.length )
 {
  verels = verels[i].split(" ");
  if( parseFloat(verels[2]) >= 7.0 )  // select problem with redering over top fix in IE 7 so return
   return;
 }
 var oSelects=document.getElementsByTagName("select");
 var count = 0;
 b1t = parseInt(Cal.elem.style.top);
 b1h = parseInt(Cal.elem.offsetHeight);
 b1l = parseInt(Cal.elem.style.left)
 b1w = parseInt(Cal.elem.offsetWidth);
 
 for(var i=0; i < oSelects.length; i++ )
 {
  b2t = parseInt(fDomOffset(oSelects[i], 'offsetTop'));
  b2h = parseInt(oSelects[i].offsetHeight);
  b2l = parseInt(fDomOffset(oSelects[i], 'offsetLeft'));
  b2w = parseInt(oSelects[i].offsetWidth);
  if( b1t <= b2t && (b1t + b1h) >= b2t && b1l <= (b2l + b2w) && (b1l + b1w) >= b2l ) 
  { 
   oSelects[i].style.visibility="hidden";
   selectControls[count++] = oSelects[i]; 
  }
  else
   selectControls[count] = null;
 }
}


function Calendar(pDate,pCtrl,elem)
{
	//Properties
	this.Date=pDate.getDate();//selected date
	this.Month=pDate.getMonth();//selected month number
	this.Year=pDate.getFullYear();//selected year in 4 digits
	
		
	this.Ctrl=pCtrl;
	if( !this.Ctrl )
	 this.Ctrl={};
	if( !this.Ctrl.id )
	 this.Ctrl.id={}; 
	this.elem = elem;
	this.Format="ddMMyyyy";
	this.Separator=DateSeparator;
}

function GetMonthIndex(shortMonthName)
{
	for (i=0;i<12;i++)
	{
		if (MonthName[i].substring(0,3).toUpperCase()==shortMonthName.toUpperCase())
		{	return i;}
	}
}
Calendar.prototype.GetMonthIndex=GetMonthIndex;

function IncYear()
{	Cal.Year++;}
Calendar.prototype.IncYear=IncYear;

function DecYear()
{	Cal.Year--;}
Calendar.prototype.DecYear=DecYear;

function IncMonth()
{	
 Cal.Month++;
 if( Cal.Month == 12 )
 {
  Cal.Month = 0;
  Cal.Year++;
 }
}
Calendar.prototype.IncMonth=IncMonth;

function DecMonth()
{
 Cal.Month--;
 if( Cal.Month < 0 )
 {
  Cal.Month = 11;
  Cal.Year--;
 }
}
Calendar.prototype.DecMonth=DecMonth;
	
function SwitchMth(intMth)
{	Cal.Month=intMth;}
Calendar.prototype.SwitchMth=SwitchMth;

function GetMonthName(IsLong,nextPrev)
{
 var Month=MonthName[this.Month];
 if( nextPrev < 0 )
 {
  if( this.Month == 0 )
	  Month=MonthName[11];
	 else 
   Month=MonthName[this.Month-1];
 }
 if( nextPrev > 0 )
 {
  if( this.Month == 11 )
	  Month=MonthName[0];
	 else 
   Month=MonthName[this.Month+1];
 }
	if (IsLong)
		return Month;
	else
		return Month.substr(0,3);
}
Calendar.prototype.GetMonthName=GetMonthName;

function GetMonDays()//Get number of days in a month
{
	var DaysInMonth=[31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
	if (this.IsLeapYear())
	{
		DaysInMonth[1]=29;

	}	
	return DaysInMonth[this.Month];	
}
Calendar.prototype.GetMonDays=GetMonDays;

function IsLeapYear()
{
	if ((this.Year%4)==0)
	{
		if ((this.Year%100==0) && (this.Year%400)!=0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	else
	{
		return false;
	}
}
Calendar.prototype.IsLeapYear=IsLeapYear;

function FormatDate(pDate)
{
	if (this.Format.toUpperCase()=="DDMMYYYY")
		return (pDate+DateSeparator+(this.Month+1)+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="DDMMMYYYY")
		return (pDate+DateSeparator+this.GetMonthName(false)+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMDDYYYY")
		return ((this.Month+1)+DateSeparator+pDate+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMMDDYYYY")
		return (this.GetMonthName(false)+DateSeparator+pDate+DateSeparator+this.Year);			
}
Calendar.prototype.FormatDate=FormatDate;	
