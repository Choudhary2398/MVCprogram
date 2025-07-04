

$(function () {
    loadCountries();  // Load country options in dropdown
    loadData();       // Load existing registrations

    // Save or update registration
    $('#saveBtn').click(function () {
        let reg = {
            Id: $('#Id').val(),
            Name: $('#Name').val(),
            Number: $('#Number').val(),
            Email: $('#Email').val(),
            CountryID: $('#CountryID').val()
        };

        // Basic validation
        if (!reg.Name || !reg.Number || !reg.Email || !reg.CountryID) {
            alert("Please fill all fields including Country.");
            return;
        }

        $.post('/Home/Save', reg, function () {
            resetForm();
            loadData();
            const modal = bootstrap.Modal.getInstance(document.getElementById('regModal'));
            modal.hide();
        });
    });

    // Handle edit button using data attributes
    $(document).on('click', '.edit-btn', function () {
        debugger
        let id = $(this).data('id');
        let name = $(this).data('name');
        let number = $(this).data('number');
        let email = $(this).data('email');
        let countryID = $(this).data('countryid');

        $('#Id').val(id);
        $('#Name').val(name);
        $('#Number').val(number);
        $('#Email').val(email);
        $('#CountryID').val(countryID);

        let modal = new bootstrap.Modal(document.getElementById('regModal'));
        modal.show();
    });
});

// Load registrations and render to table
function loadData() {
    $.get('/Home/GetAll', function (res) {
        debugger
        let html = '';
        $.each(res, function (i, v) {
            html += `
                <tr>
                    <td>${v.Name}</td>
                    <td>${v.Number}</td>
                    <td>${v.Email}</td>
                    <td>${v.Country?.CountryName || ''}</td>
                    <td>
                        <button 
                            class="btn btn-sm btn-primary me-1 edit-btn"
                            data-id="${v.Id}"
                            data-name="${v.Name}"
                            data-number="${v.Number}"
                            data-email="${v.Email}"
                            data-countryid="${v.CountryID}">
                            <i class="bi bi-pencil"></i> Edit
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="remove(${v.Id})">
                            <i class="bi bi-trash"></i> Delete
                        </button>
                    </td>
                </tr>`;
        });
        $('#regTable tbody').html(html);
    });
}

// Delete registration
function remove(id) {
    if (confirm("Are you sure?")) {
        $.post('/Home/Delete', { id: id }, function () {
            loadData();
        });
    }
}

// Reset form fields
function resetForm() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Number').val('');
    $('#Email').val('');
    $('#CountryID').val('');
}

// Load country options into the dropdown
function loadCountries() {
    $.get('/Country/GetAll', function (res) {
        let options = '<option value="">-- Select Country --</option>';
        $.each(res, function (i, v) {
            options += `<option value="${v.CountryID}">${v.CountryName}</option>`;
        });
        $('#CountryID').html(options);
    });
}