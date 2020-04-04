import os
import json
import copy
from flask import Flask, flash, request, redirect, url_for
from werkzeug.utils import secure_filename
from os.path import join, dirname, realpath
from multiprocessing import Process, Queue
app = Flask(__name__)
import uuid


app.config['UPLOAD_FOLDER'] = join(dirname(realpath(__file__)), '../sounds/')
app.secret_key = "XDDD"

ALLOWED_EXTENSIONS = {"wav"}



from scipy.io.wavfile import read
from twilio.rest import Client


def make_call():

    # Your Account Sid and Auth Token from twilio.com/console
    # DANGER! This is insecure. See http://twil.io/secure


    print("Calling...")
    pass


class AnomalyDetector:

    def __init__(self, base):
        self.samprate_base, self.wavdata_base = self.get_wavdata(base)

    def get_wavdata(self, file):
        samprate, wavdata = read(file)
        return samprate, wavdata

    def calc_distances(self, samprate, values, min_val=5000, focus_const=0.15):
        focus_size = int(focus_const * samprate)
        data_size = len(values)

        focuses = []
        distances = []
        index = 0

        while index < data_size:
            if values[index] > min_val:
                mean_idx = index + focus_size // 2
                focuses.append(float(mean_idx) / data_size)
                if len(focuses) > 1:
                    last_focus = focuses[-2]
                    actual_focus = focuses[-1]
                    distances.append(actual_focus - last_focus)
                index += focus_size
            else:
                index += 1
        return distances

    def detect_anomaly(self, pattern, test, min_error):
        if len(pattern) == 0 and len(test) == 0:
            return False
        for i, dt in enumerate(pattern):
            if not dt - test[i] < min_error:
                return False
        return True

    def is_anomaly(self, new):

        samprate_base, wavdata_base = self.samprate_base, self.wavdata_base
        samprate_new, wavdata_new = self.get_wavdata(new)

        #reduce noise on new file using baseline audio
        #wavdata_new = nr.reduce_noise(audio_clip=wavdata_new, noise_clip=wavdata_base, verbose=True)

        if wavdata_new.shape[1] == 2:
            wavdata_new = wavdata_new[:, 0]

        pattern = self.calc_distances(samprate_base, wavdata_base)
        test = self.calc_distances(samprate_new, wavdata_new)
        print(pattern, test)
        return self.detect_anomaly(pattern, test, 0.1)


BASE = "../storage/base.wav"

def data_analyzing(queue):
    print("Analyzing data")
    anomaly_detector = AnomalyDetector(BASE)
    path = "../storage/"
    while True:
        file = queue.get()
        # try:
        v = anomaly_detector.is_anomaly(path + file)
        # except:
        #     v = False
        os.remove(path + file)
        if v:
            make_call()


fileQueue = Queue()
process = Process(target=data_analyzing, args=(fileQueue, ))
process.daemon = True
process.start()



def allowed_file(filename):
    return '.' in filename and \
           filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


@app.route('/uploadBreath/', methods=['POST', 'GET'])
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
            id = str(uuid.uuid1()) + ".wav"
            file.save("../storage/" + id)
            fileQueue.put(id)
            return json.dumps({'success': True}), 200, {'ContentType': 'application/json'}
    return '''
    <!doctype html>
    <title>Upload new File</title>
    <h1>Upload new File</h1>
    <form method=post enctype=multipart/form-data>
      <input type=file name=file>
      <input type=submit value=Upload>
    </form>
    '''

# if __name__ == "main":
    # Your Account Sid and Auth Token from twilio.com/console
    # DANGER! This is insecure. See http://twil.io/secure


app.run(threaded=False, debug=True)