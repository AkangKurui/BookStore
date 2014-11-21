$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "http://localhost/api-bookstore/api/invoice",
        success: function (results) {
            results.forEach(function (result) {
                $("[table3]").append("<tr id='" + result.Id + "'><td>" +
                    result.InvoiceNo + "</td><td>" + result.Customer.Name +
                    "</td><td class='text-right'>" + result.Customer.Term.Value +
                    "</td><td class='text-right'>" + result.TransactionDateString +
                    "</td><td class='text-right'>" + result.DueDateString +
                    "</td><td class='text-right'>" + result.SubTotal +
                    "</td><td class='text-right'>" + result.NetTotal +
                    "</td><td><button id = ' " + result.Id +
                    "' class='btn btn-xs btn-danger btn-del'>Del</button><button id = ' " + result.Id +
                    "' class='btn btn-xs btn-success btn-show'>Show</button></td></tr>");
            });
        }
    })

    $("[table2]").on("click", "tr", function (ev) {
        var itemId = ev.currentTarget.id;
        $('.btn-qtySave').val(itemId);
        $('#qtyModal').modal('show');
    });

    $(".btn-qtySave").on("click", function (ev) {
        var itemId = $('.btn-qtySave').val();
        var transId = $("#invId").attr("value");
        var qty = $("#modalQty").val();
        var total = $("tr#" + itemId + " td:nth-child(4)").text() * qty;
        $.ajax({
            type: "PUT",
            dataType: 'json',
            url: "http://localhost/api-bookstore/api/invoice/"+transId,
            data: { itemId: itemId, id: transId, qty: qty },
            success: function () {
                $('#qtyModal').modal('hide');
                $("tr#" + itemId + " td:nth-child(3)").text(qty);
                $("tr#" + itemId + " td:nth-child(5)").text(total);
            },
            error: function (a, b, c, d) { }
        })
    });

    $(document).on("click",".btn-del",function () {
        var transId = this.id;
        $.ajax({
            type: "DELETE",
            url: "http://localhost/api-bookstore/api/invoice",
            dataType: "json",
            data: { id: transId },
            success: function () {
                $("tr#" + transId).remove()
            }
        })
    });

    $(".btn-date").click(function () {
        $('#dateModal').modal('show');
    });

    $(".btn-dateSave").click(function () {
        var transId = $("#invId").attr("value");
        var date = $("#modalDate").val();
        $.ajax({
            type: "PUT",
            dataType: "json",
            contentType: "application/json",
            url: "http://localhost/api-bookstore/api/invoice/" + transId,
            data: { Date: date },
            success: function (result) {
                $('#dateModal').modal('hide');
                $("div#transactionDate span").text(result.TransactionDateString)
                $("div#dueDate span").text(result.DueDateString)
            },
            error: function (a, b, c, d) { }
        })
    });

    $(".btn-invoice").click(function (ev) {
        ev.preventDefault();
        var name = $("#listInvoice input").val();
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "http://localhost/api-bookstore/api/invoice",
            data: { Name: name },
            success: function (result) {
                $("[table3]").append("<tr id='" + result.Id + "'><td>" +
                    result.InvoiceNo + "</td><td>" + result.Customer.Name +
                    "</td><td class='text-right'>" + result.Customer.Term.Value +
                    "</td><td class='text-right'>" + result.TransactionDateString +
                    "</td><td class='text-right'>" + result.DueDateString +
                    "</td><td class='text-right'>" + result.SubTotal +
                    "</td><td class='text-right'>" + result.NetTotal +
                    "</td><td><button id = ' " + result.Id +
                    "' class='btn btn-xs btn-danger btn-del'>Del</button><button id = ' " + result.Id +
                    "' class='btn btn-xs btn-success btn-show'>Show</button></td></tr>");
            },
            error: function (a, b, c) {
            }
        })
    });

    $(document).on("click", ".btn-show", function () {
        var transId = this.id;
        $.ajax({
            type: "GET",
            url: "http://localhost/api-bookstore/api/invoice",
            data: { id: transId },
            success: function (result) {
                $("#listInvoice").hide();
                $("#invoice").show();
                $("#invId").attr("value", result.Id);
                $("#custName").append(result.Customer.Name);
                $("#custAddress").append(result.Customer.Address);
                $("#invNo").append(result.InvoiceNo);
                $("#transactionDate").append("<span>" + result.TransactionDateString + "</span>")
                $("#dueDate").append("<span>" + result.DueDateString + "</span>")
                $("#subTotal").append("<span>" + result.SubTotal + "</span>")
                $("#netTotal").append("<span>" + result.NetTotal + "</span>")
            }
        });

        $.ajax({
            type: "GET",
            url: "http://localhost/api-bookstore/api/invoice/" + transId +"/items",
            success: function (results) {
                results.forEach(function (result) {
                    $("[table2]").append("<tr id='" + result.ItemId + "'><td>" +
                        result.Name + "</td><td>" +
                        result.Barcode + "</td><td class='text-right'>" +
                        result.Qty + "</td><td class='text-right'>" +
                        result.Price + "</td><td class='text-right'>" +
                        result.Total + "</td></tr>");
                });
            }
        });
    });

    $(".btn-item").click(function () {
        $('#myModal').modal('show')
    });

    $(".btn-itemSave").click(function () {
        var transId = $("#invId").attr("value");
        var itemName = $("#itemName").val();
        var qty = $("#qty").val();
        var price = $("#price").val();
        var barcode = $("#barcode").val();
        $.ajax({
            type: "POST",
            url: "http://localhost/api-bookstore/api/invoice/" +transId,
            data: {
                id: transId,
                name: itemName,
                qty: qty,
                price: price,
                barcode: barcode
            },
            success: function (result) {
                $('#myModal').modal('hide');
                $("[table2]").append("<tr id='" + result.ItemId + "'><td>" +
                    result.Name + "</td><td>" +
                    result.Barcode + "</td><td class='text-right'>" +
                    result.Qty + "</td><td class='text-right'>" +
                    result.Price + "</td><td class='text-right'>" +
                    result.Total + "</td></tr>");
            }
        })
    });
});