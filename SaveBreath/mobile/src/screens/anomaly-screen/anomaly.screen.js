import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Text, Button, Layout } from '@ui-kitten/components';

import LayoutWithStatusBar from '../../components/layout-with-status-bar';

import { RecordingService } from '../../services/recording.service';

const AnomalyScreen = ({ navigation }) => {
  const sendRequest = async () => {
    try {
      console.log('request sended');
      await RecordingService.upload();
      await navigation.navigate('HomeScreen');
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <LayoutWithStatusBar layoutStyle={styles.layout} barStyle="light-content">
      <Text category="h1">Anomaly detected</Text>
      <Text category="h5" styles={styles.margins}>
        Should we call for help?
      </Text>
      <Layout styles={styles.row}>
        <Button
          status="danger"
          appearance="filled"
          style={styles.btnCall}
          onPress={async () => sendRequest}>
          Yes!
        </Button>
        <Button
          status="success"
          appearance="filled"
          style={styles.btnIgnore}
          onPress={() => navigation.navigate('RecordScreen')}>
          No, I'm ok.
        </Button>
      </Layout>
    </LayoutWithStatusBar>
  );
};

const styles = StyleSheet.create({
  layout: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  margins: {
    margin: 8,
  },
  row: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center',
    alignItems: 'center',
  },
  btnCall: {
    marginTop: 8,
    width: 125,
  },
  btnIgnore: {
    marginTop: 8,
    width: 125,
  },
});

export default AnomalyScreen;
