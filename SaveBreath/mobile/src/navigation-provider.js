import * as React from 'react';
import { createStackNavigator } from '@react-navigation/stack';

import Screens from './screens';

const { Navigator, Screen } = new createStackNavigator();

const sharedStatusBarOptions = {
  headerStatusBarHeight: 0,
  headerStyle: {
    backgroundColor: '#1e263c',
  },
  headerTintColor: '#1e263c',
};

const NavigationProvider = () => (
  <Navigator>
    <Screen
      name="HomeScreen"
      component={Screens.HomeScreen}
      options={{ ...sharedStatusBarOptions }}
    />
    <Screen
      name="RecordScreen"
      component={Screens.RecordScreen}
      options={{ ...sharedStatusBarOptions }}
    />
    <Screen
      name="AnomalyScreen"
      component={Screens.AnomalyScreen}
      options={{ ...sharedStatusBarOptions }}
    />
  </Navigator>
);

export default NavigationProvider;
