import os
import json
from flask import Flask, flash, request, redirect, url_for
from werkzeug.utils import secure_filename
from os.path import join, dirname, realpath
from multiprocessing import Process
app = Flask(__name__)


app.config['UPLOAD_FOLDER'] = join(dirname(realpath(__file__)), 'sounds/')
app.secret_key = "XDDD"

ALLOWED_EXTENSIONS = {"wav"}


process = Process(target=f, args=())


def allowed_file(filename):
    return '.' in filename and \
           filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


@app.route('/uploadBreath/', methods=['POST'])
def upload_breath():
    if request.method == 'POST':
        if 'file' not in request.files:
            flash('No file part')
            return redirect(request.url)
        file = request.files['file']
        if file.filename == '':
            flash('No selected file')
            return redirect(request.url)
        if file and allowed_file(file.filename):
            filename = secure_filename(file.filename)
            file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
            return json.dumps({'success': True}), 200, {'ContentType': 'application/json'}



# Check breath #*
# Wysyłanie powiadomienia jesli nie
# Wykonanie połączenia jesli nie
# Jesli nie informacja do pogotowia

app.run()