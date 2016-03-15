(function () {
   
    $(document).ready(function () {
       
        var table = $('#advTable').dataTable({
            "ajaxSource": "Advertisement/GetAll",         
            "columns": [             
                { "data": "Id" },
                { "data": "Height" },
                { "data": "Width" },
                { "data": "Lt" },
                { "data": "Ln" },
                { "data": "Description" },
                { "data": "MaintenanceTime" },
                { "data": "Type" },
                { "data": "MonthlyCost", render: $.fn.dataTable.render.number(',', '.', 2, '$') },
                {
                    data: null,
                    className: "center",
                    defaultContent: '<button id="editRow" title="Edit row" type="button" class="btn btn-default"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button>' +
                        '<button id="delRow" title="Delete row" type="button" class="btn btn-default"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button> '
                }
            ],
            "columnDefs": [
                { "targets": [-1], "sortable": false, "searchable": false },
                { "targets": [0], "visible": false, "searchable": false },
                { "targets": [1], "visible": false, "searchable": false },
                { "targets": [2], "visible": false, "searchable": false },
                { "targets": [3], "visible": false, "searchable": false },
                { "targets": [4], "visible": false, "searchable": false },
            ],
            //"select": true,           
        });
        
        var Lat; var Long;

        $('#advTable tbody').off().on('click', 'tr td:not(:last-child)', function () {
            //get all data with hidden cols
            var position = table.fnGetPosition(this);
            var rowData = table.fnGetData(position);
            //set location coords to global vars
            Lat = rowData.Lt;
            Long = rowData.Ln;
            //refresh modal values with current item
            $('.modal-title').text('').append(rowData.Description);
            $('.advType').text('').append(rowData.Type);
            $('.advHeight').text('').append(rowData.Height + ' m');
            $('.advWidth').text('').append(rowData.Width + ' m');
            $('.advTime').text('').append(rowData.MaintenanceTime);
            $('.advCost').text('').append(rowData.MonthlyCost + '$');

            $('#modalInfo').modal();
        });

        $('#modalInfo').on('shown.bs.modal', function () {
            GetMap();
        });

        function GetMap() {
            google.maps.visualRefresh = true;
            var coords = new google.maps.LatLng(Long, Lat);
            var mapOptions = {
                zoom: 12,
                center: coords,
                mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
            };
            var map = new google.maps.Map(document.getElementById("map"), mapOptions);
            var marker = new google.maps.Marker({
                position: coords,
                map: map,
                title: 'Constraction location'
            });
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');
        }
    });
})();