import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Text, Layout } from '@ui-kitten/components';

import LayoutWithStatusBar from '../../components/layout-with-status-bar';
import Recorder from '../../components/recorder';

class RecordScreen extends React.Component {
  state = {
    timerValue: null,
  };

  handleRecord = isRecording =>
    console.log(isRecording ? 'startRecording' : 'finishRecording');

  render() {
    return (
      <LayoutWithStatusBar layoutStyle={styles.layout} barStyle="light-content">
        <Text category="h1">Record screen</Text>
        <Layout style={styles.layout}>
          <Recorder record={this.handleRecord} />
        </Layout>
      </LayoutWithStatusBar>
    );
  }
}

const styles = StyleSheet.create({
  layout: {
    flex: 1,
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default RecordScreen;
