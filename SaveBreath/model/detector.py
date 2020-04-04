from scipy.io.wavfile import read
import noisereduce as nr

class AnomalyDetector:
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

    def is_anomaly(self, base, new):

        samprate_base, wavdata_base = self.get_wavdata(base)
        samprate_new, wavdata_new = self.get_wavdata(new)

        #reduce noise on new file using baseline audio
        #wavdata_new = nr.reduce_noise(audio_clip=wavdata_new, noise_clip=wavdata_base, verbose=True)

        pattern = self.calc_distances(samprate_base, wavdata_base)
        test = self.calc_distances(samprate_new, wavdata_new)
        print(pattern, test)
        return self.detect_anomaly(pattern, test, 0.1)

anomaly_detector = AnomalyDetector()
print(anomaly_detector.is_anomaly("base.wav", "noise.wav"))