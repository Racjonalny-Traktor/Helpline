import * as React from 'react';
import { StyleSheet } from 'react-native';
import { Button, Text, Icon } from '@ui-kitten/components';

const renderIcon = iconName => style => <Icon {...style} name={iconName} />;

const Recorder = ({ record }) => {
  const [isRecording, setRecording] = React.useState(false);
  const iconName = isRecording ? 'mic-off-outline' : 'mic-outline';
  const textContent = isRecording
    ? 'Press button when you are awake'
    : `Press to have good night's sleep`;

  function handleRecording() {
    setRecording(!isRecording);
    record(isRecording);
  }

  return (
    <React.Fragment>
      <Text category="h6" style={styles.text}>
        {textContent}
      </Text>
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
  text: {
    width: 300,
    textAlign: 'center',
  },
  button: {
    marginTop: 24,
    width: 74,
    height: 74,
    borderRadius: 37,
  },
});

export default Recorder;
