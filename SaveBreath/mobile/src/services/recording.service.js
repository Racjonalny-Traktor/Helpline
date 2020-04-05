import { apiService, API_URL } from '../utils/api.service';
import { data } from '../utils/base';

const upload = async () => {
  try {
    const res = await apiService.post(
      API_URL.UPLOAD_BREATH,
      { data },
      {
        headers: { 'Content-Type': 'application/json' },
      },
    );

    console.log(res);
  } catch (err) {
    alert(err);
  }
};

export const RecordingService = { upload };
