import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Button, Text, Icon } from '@ui-kitten/components';

const renderIcon = iconName => style => <Icon {...style} name={iconName} />;

const Recorder = ({ record }) => {
  const [isRecording, setRecording] = React.useState(false);
  const [timerValue, setTimerValue] = React.useState('00:00');
  const iconName = isRecording ? 'mic-outline' : 'mic-off-outline';

  function handleRecording() {
    console.log('xd');
    setRecording(!isRecording);
  }

  return (
    <React.Fragment>
      <Text category="h3">{timerValue}</Text>
      <Button
        status="info"
        appearance="filled"
        style={styles.button}
        icon={renderIcon(iconName)}
        onPress={() => handleRecording()}
      />
    </React.Fragment>
  );
};

const styles = StyleSheet.create({
  button: {
    marginTop: 16,
    width: 74,
    height: 74,
    borderRadius: 37,
  },
});

export default Recorder;
