import * as React from 'react';
import { createStackNavigator } from '@react-navigation/stack';

import Screens from './screens';

const { Navigator, Screen } = new createStackNavigator();

const NavigationProvider = () => (
  <Navigator>
    <Screen
      name="HomeScreen"
      component={Screens.HomeScreen}
      options={{
        headerStatusBarHeight: 0,
        headerStyle: {
          backgroundColor: '#1e263c',
        },
        headerTintColor: '#1e263c',
      }}
    />
    <Screen
      name="RecordScreen"
      component={Screens.RecordScreen}
      options={{
        headerStatusBarHeight: 0,
        headerStyle: {
          backgroundColor: '#1e263c',
        },
        headerTintColor: '#1e263c',
      }}
    />
  </Navigator>
);

export default NavigationProvider;
