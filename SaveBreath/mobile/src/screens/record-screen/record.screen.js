import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Text, Layout } from '@ui-kitten/components';

import LayoutWithStatusBar from '../../components/layout-with-status-bar';
import Recorder from '../../components/recorder';

const INITIAL_STATE = {
  timerValue: null,
  isModalClosed: false,
};

class RecordScreen extends React.Component {
  state = Object.freeze(INITIAL_STATE);

  handleRecord = isRecording => {
    const { navigation } = this.props;
    if (!isRecording) {
      setTimeout(navigation.navigate('AnomalyScreen'), 1000);
    }
  };

  render() {
    return (
      <LayoutWithStatusBar layoutStyle={styles.layout} barStyle="light-content">
        <Text category="h1">Save your breath</Text>
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
