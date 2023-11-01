"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();


connection.start().then(function () {
	//alert('Connected to dashboardHub');

	InvokeProducts();
	


}).catch(function (err) {
	/*var popup = document.getElementById("tblProduct");
	popup.style.display = "none";*/

});
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		
	});
}

connection.on("ReceivedProducts", function (viewModel) {
	BindProductsToGrid(viewModel);
});

function BindProductsToGrid(viewModel) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(viewModel.tickets, function (index, ticket) {
		tr = $('<tr/>');
		tr.append(`<td>${ticket.id}</td>`);
		tr.append(`<td>${ticket.subject}</td>`);
		tr.append(`<td>${ticket.body}</td>`);
	   var bodyCell = $('<td class="body-cell expand"/>');
		$.each(ticket.attachments, function (attachmentIndex, attachment) {
			var attachmentLink = $('<a/>').attr('href', '/Ticket/DisplayAttachment?AttachmentId=' + attachment.id).text(attachment.fileName);
			bodyCell.append(attachmentLink);
			bodyCell.append('<br />');
		});

		tr.append(bodyCell);
		tr.append(`
            <td>

                <div class="btn-group more" role="group" aria-label="Basic radio toggle button group">
                    <input type="radio" class="btn-check" value="Low" name="btnradio-${ticket.id}" id="btnradio1-${ticket.id}" autocomplete="off">
                    <label class="btn btn-outline-primary more" for="btnradio1-${ticket.id}">Low</label>
                    <input type="radio" class="btn-check" value="Medium" name="btnradio-${ticket.id}" id="btnradio2-${ticket.id}" autocomplete="off">
                    <label class="btn btn-outline-primary more" for="btnradio2-${ticket.id}">Medium</label>
                    <input type="radio" class="btn-check" value="High" name="btnradio-${ticket.id}" id="btnradio3-${ticket.id}" autocomplete="off">
                    <label class="btn btn-outline-primary more" for="btnradio3-${ticket.id}">High</label>
                </div>
            </td>
        `);
		var buttonCell = $('<td/>');
		var assignButton = $('<button/>').attr('type', 'button').addClass('btn btn-success assign-button').attr('data-id', ticket.id).text('Assign');
		buttonCell.append(assignButton);
		tr.append(buttonCell);


		$('#tblProduct').append(tr);
	});
}

/*document.addEventListener("click", function (e) {
	if (e.target && e.target.classList.contains("btn-success")) {
		
	}
});*/ 
/*$(function () {

	connection.start().then(function () {
	//alert('Connected to dashboardHub');

		InvokeProducts();
	

	}).catch(function (err) {
		return console.error(err.toString());
		
	});
});*/
/*
// Product
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
	BindProductsToGrid(products);
});

function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(products, function (index, product) {
		tr = $('<tr/>');
**//*//*		tr.append(`<td>${(product.Id)}</td>`);
		tr.append(`<td>${product.Subject}</td>`);
		tr.append(`<td>${product.Body}</td>`);**//*//*
		tr.append(`<td>${"help me"}</td>`);
		tr.append(`<td>${"plaese work"}</td>`);
		tr.append(`<td>${"plaese"}</td>`);
		$('#tblProduct').append(tr);
	});
}*/

