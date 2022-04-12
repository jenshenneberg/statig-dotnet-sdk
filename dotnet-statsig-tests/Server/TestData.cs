﻿using System;

namespace dotnet_statsig_tests
{
    public abstract class TestData
    {
        public static string layerInitialize = @"
            {
              'feature_gates': {},
              'dynamic_configs': {},
              'layer_configs': {
                '7KbKUZW/yEHFyPbl8k69KMGsZH46ncEaVybX1zSJPh0=': {
                  'name': '7KbKUZW/yEHFyPbl8k69KMGsZH46ncEaVybX1zSJPh0=',
                  'value': {
                    'a_string': 'a_value'
                  },
                  'rule_id': '4wAsx5uAYntXGDy7jEwfb6',
                  'group': '4wAsx5uAYntXGDy7jEwfb6',
                  'allocated_experiment_name': 'PGxbC3EpEOjMyuU7Rg2s+leE0uRnj25OfzOqKcBWLs4=',
                  'is_device_based': false,
                  'is_experiment_active': true,
                  'explicit_parameters': [],
                  'is_user_in_experiment': true,
                  'secondary_exposures': [],
                  'undelegated_secondary_exposures': []
                }
              },
              'sdkParams': {},
              'has_updates': true,
              'time': 1648749618359
            }
        ";

        public static string layerExposuresDownloadConfigSpecs = @"
			{
              'has_updates': true,
              'feature_gates': [],
              'dynamic_configs': [
                {
                  '__________________________USED_BY_TESTS': [
                    'test_explicit_vs_implicit_parameter_logging'
                  ],
                  'name': 'experiment',
                  'type': 'dynamic_config',
                  'salt': '58d0f242-4533-4601-abf7-126aa8f43868',
                  'enabled': true,
                  'defaultValue': {
                    'an_int': 0,
                    'a_string': 'layer_default'
                  },
                  'rules': [
                    {
                      'name': 'alwaysPass',
                      'groupName': 'Public',
                      'passPercentage': 100,
                      'conditions': [
                        {
                          'type': 'public',
                          'targetValue': null,
                          'operator': null,
                          'field': null,
                          'additionalValues': {},
                          'isDeviceBased': false,
                          'idType': 'userID'
                        }
                      ],
                      'returnValue': {
                        'an_int': 99,
                        'a_string': 'exp_value'
                      },
                      'id': 'alwaysPass',
                      'salt': '',
                      'isDeviceBased': false,
                      'idType': 'userID'
                    }
                  ],
                  'isDeviceBased': false,
                  'idType': 'userID',
                  'entity': 'experiment',
                  'explicitParameters': [
                    'an_int'
                  ]
                }
              ],
              'layer_configs': [
                {
                  '__________________________USED_BY_TESTS': [
                    'test_does_not_log_on_get_layer',
                    'test_does_not_log_on_invalid_type',
                    'test_does_not_log_non_existent_keys',
                    'test_unallocated_layer_logging',
                    'test_logs_user_and_event_name'
                  ],
                  'name': 'unallocated_layer',
                  'type': 'dynamic_config',
                  'salt': '3e361046-bc69-4dfd-bbb1-538afe609157',
                  'enabled': true,
                  'defaultValue': {
                    'an_int': 99
                  },
                  'rules': [],
                  'isDeviceBased': false,
                  'idType': 'userID',
                  'entity': 'layer'
                },
                {
                  '__________________________USED_BY_TESTS': [
                    'test_explicit_vs_implicit_parameter_logging'
                  ],
                  'name': 'explicit_vs_implicit_parameter_layer',
                  'type': 'dynamic_config',
                  'salt': '3e361046-bc69-4dfd-bbb1-538afe609157',
                  'enabled': true,
                  'defaultValue': {
                    'an_int': 0,
                    'a_string': 'layer_default'
                  },
                  'rules': [
                    {
                      'name': 'experimentAssignment',
                      'groupName': 'Experiment Assignment',
                      'passPercentage': 100,
                      'conditions': [
                        {
                          'type': 'public',
                          'targetValue': null,
                          'operator': null,
                          'field': null,
                          'additionalValues': {},
                          'isDeviceBased': false,
                          'idType': 'userID'
                        }
                      ],
                      'returnValue': {
                        'an_int': 0,
                        'a_string': 'layer_default'
                      },
                      'id': 'experimentAssignment',
                      'salt': '',
                      'isDeviceBased': false,
                      'idType': 'userID',
                      'configDelegate': 'experiment'
                    }
                  ],
                  'isDeviceBased': false,
                  'idType': 'userID',
                  'entity': 'layer'
                },
                {
                  '__________________________USED_BY_TESTS': [
                    'test_different_object_type_logging'
                  ],
                  'name': 'different_object_type_logging_layer',
                  'type': 'dynamic_config',
                  'salt': '3e361046-bc69-4dfd-bbb1-538afe609157',
                  'enabled': true,
                  'defaultValue': {
                    'a_bool': true,
                    'an_int': 99,
                    'a_double': 1.23,
                    'a_long': 9223372036854776000,
                    'a_string': 'value',
                    'an_array': [
                      'a',
                      'b'
                    ],
                    'an_object': {
                      'key': 'value'
                    },
                    'another_object': {
                      'another_key': 'another_value'
                    }
                  },
                  'rules': [],
                  'isDeviceBased': false,
                  'idType': 'userID',
                  'entity': 'layer'
                }
              ]
            }
		";
    }
}
