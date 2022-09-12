
print "Content-Type:text/javascript"
print


import shapefile
import sys
reload(sys)
sys.setdefaultencoding('utf-8') 
sf = shapefile.Reader("/content/Map/jinchuan.shp")
shapes = sf.shapes()

first_str = '''
	var map;
    var zoom = 9;
    map = new T.Map('mapDiv', {
        projection: 'EPSG:32650'
    });
    map.centerAndZoom(new T.LngLat(112.858577, 35.497694), zoom);

'''
name_str=''''''
for num in range(len(shapes)):
	name_str=name_str+'''
		points_autao%d=[];
	'''% (num)

first_str=first_str+name_str

polyline_str = ''''''
for shp in range(len(shapes)):
    shap = shapes[shp]#一个面文件，里面可能有很多ploygon,shapes[shp]就是一个，一共有len(shapes)个ploygon
    for i in range(len(shap.points)):#读每个ploygon中点坐标
        polyline_str=polyline_str+'''
            points_autao%d.push(new T.LngLat(%f, %f));
        '''% (shp,shap.points[i][0],shap.points[i][1])


finally_str=''
for num in range(len(shapes)):
	finally_str=finally_str+'''
	    var polygon_autao%d = new T.Polygon(points_autao%d,{
	                color: "blue", weight: 3, opacity: 0.5, fillColor: "red", fillOpacity: 0.5
	            });
	    map.addOverLay(polygon_autao%d);
	'''% (num,num,num)

print first_str+polyline_str+finally_str
#必须要挨个读取ploygon，不能一次读取，不然画出来的图形就乱了。一次压入所有点，就依点画，明显不对。
