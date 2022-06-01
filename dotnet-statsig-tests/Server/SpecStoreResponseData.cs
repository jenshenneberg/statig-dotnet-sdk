﻿using System.Collections.Generic;
using Statsig.Lib;

namespace dotnet_statsig_tests
{
	public class SpecStoreResponseData
	{
        internal static string getIDList1Response(int index)
        {
            var responses = new string[]
            {
                "+1\n",
                "-1\n+2\n",
                "+3\n",
                "3",
                "+3\n+4\n+5\n+4\n-4\n+6\n+6\n+5\n",
            };
            return index >= responses.Length ? responses[responses.Length - 1] : responses[index];
        }

        internal static Dictionary<string, IDList[]> getIDListExpectedResults(string baseURL)
        {
            var expectedList1 = new IDList[]
            {
                idListWithIDs("list_1", 3, baseURL + "/list_1", 1, "file_id_1", new string[] { "1" }),
                idListWithIDs("list_1", 9, baseURL + "/list_1", 1, "file_id_1", new string[] { "2" }),
                idListWithIDs("list_1", 3, baseURL + "/list_1", 3, "file_id_1_a", new string[] { "3" }),
                idListWithIDs("list_1", 3, baseURL + "/list_1", 3, "file_id_1_a", new string[] { "3" }),
                null,
                idListWithIDs("list_1", 24, baseURL + "/list_1", 3, "file_id_1_a", new string[] { "3", "5", "6" }),
            };
            var expectedList2 = new IDList[]
            {
                idListWithIDs("list_2", 3, baseURL + "/list_2", 1, "file_id_2", new string[] { "a" }),
                null,
                null,
                null,
                null,
                null,
            };
            var expectedList3 = new IDList[]
            {
                null,
                null,
                null,
                null,
                idListWithIDs("list_3", 3, baseURL + "/list_3", 5, "file_id_3", new string[] { "0" }),
                idListWithIDs("list_3", 3, baseURL + "/list_3", 5, "file_id_3", new string[] { "0" }),
            };
            return new Dictionary<string, IDList[]>
            {
                {"list_1", expectedList1 },
                {"list_2", expectedList2 },
                {"list_3", expectedList3 },
            };
        }

        private static IDList idListWithIDs(string name, double size, string url, double creationTime, string fileID, string[] ids)
        {
            var list = new IDList
            {
                Name = name,
                Size = size,
                URL = url,
                CreationTime = creationTime,
                FileID = fileID,
            };
            foreach (var id in ids)
            {
                list.Store.Add(id);
            }
            return list;
        }

        internal static string getIDListsResponse(string baseURL, int index)
        {
            var url1 = baseURL + "/list_1";
            var url2 = baseURL + "/list_2";
            var url3 = baseURL + "/list_3";
            var responses = new string[]
            {
                // 0
                $@"{{
                    'list_1': {{
                        'name': 'list_1',
                        'size': 3,
                        'url': '{url1}',
                        'creationTime': 1,
                        'fileID': 'file_id_1',
                    }},
                    'list_2': {{
                        'name': 'list_2',
                        'size': 3,
                        'url': '{url2}',
                        'creationTime': 1,
                        'fileID': 'file_id_2',
                    }},
                }}",
                // 1
                $@"{{
                    'list_1': {{
                        'name': 'list_1',
                        'size': 9,
                        'url': '{url1}',
                        'creationTime': 1,
                        'fileID': 'file_id_1',
                    }},
                }}",
                // 2
                $@"{{
                    'list_1': {{
                        'name': 'list_1',
                        'size': 3,
                        'url': '{url1}',
                        'creationTime': 3,
                        'fileID': 'file_id_1_a',
                    }},
                }}",
                // 3
                $@"{{
                    'list_1': {{
                        'name': 'list_1',
                        'size': 9,
                        'url': '{url1}',
                        'creationTime': 1,
                        'fileID': 'file_id_1',
                    }},
                }}",
                // 4
                $@"{{
                    'list_1': {{
                        'name': 'list_1',
                        'size': 24,
                        'url': '{url1}',
                        'creationTime': 3,
                        'fileID': 'file_id_1_a',
                    }},
                    'list_3': {{
                        'name': 'list_3',
                        'size': 3,
                        'url': '{url3}',
                        'creationTime': 5,
                        'fileID': 'file_id_3',
                    }},
                }}",
            };
            return index >= responses.Length ? responses[responses.Length - 1] : responses[index];
        }
        
        internal static string downloadConfigSpecResponse =
            @"
{
  'dynamic_configs': [
    {
      'name': 'test_config',
      'type': 'dynamic_config',
      'salt': '50ad5c60-9e7a-42ce-86c6-c49035185b14',
      'enabled': true,
      'defaultValue': { 'number': 4, 'string': 'default', 'boolean': true },
      'entity': 'dynamic_config',
      'rules': [
        {
          'name': '1kNmlB23wylPFZi1M0Divl',
          'groupName': 'statsig email',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_field',
              'targetValue': ['@statsig.com'],
              'operator': 'str_contains_any',
              'field': 'email',
              'additionalValues': {}
            }
          ],
          'returnValue': { 'number': 7, 'string': 'statsig', 'boolean': false },
          'id': '1kNmlB23wylPFZi1M0Divl',
          'salt': 'f2ac6975-174d-497e-be7f-599fea626132'
        }
      ]
    },
    {
      'name': 'sample_experiment',
      'type': 'dynamic_config',
      'salt': 'f8aeba58-18fb-4f36-9bbd-4c611447a912',
      'enabled': true,
      'defaultValue': { 'experiment_param': 'control' },
      'entity': 'experiment',
      'rules': [
        {
          'name': '',
          'groupName': 'experimentSize',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_bucket',
              'targetValue': [
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65,
                66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81,
                82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97,
                98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
                111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,
                124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136,
                137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
                150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162,
                163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175,
                176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188,
                189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201,
                202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214,
                215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227,
                228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240,
                241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253,
                254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266,
                267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279,
                280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292,
                293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305,
                306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318,
                319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331,
                332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344,
                345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357,
                358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370,
                371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383,
                384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396,
                397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409,
                410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422,
                423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435,
                436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448,
                449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461,
                462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474,
                475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487,
                488, 489, 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500,
                501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513,
                514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526,
                527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539,
                540, 541, 542, 543, 544, 545, 546, 547, 548, 549, 550, 551, 552,
                553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565,
                566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578,
                579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591,
                592, 593, 594, 595, 596, 597, 598, 599, 600, 601, 602, 603, 604,
                605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616, 617,
                618, 619, 620, 621, 622, 623, 624, 625, 626, 627, 628, 629, 630,
                631, 632, 633, 634, 635, 636, 637, 638, 639, 640, 641, 642, 643,
                644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654, 655, 656,
                657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 667, 668, 669,
                670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682,
                683, 684, 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695,
                696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708,
                709, 710, 711, 712, 713, 714, 715, 716, 717, 718, 719, 720, 721,
                722, 723, 724, 725, 726, 727, 728, 729, 730, 731, 732, 733, 734,
                735, 736, 737, 738, 739, 740, 741, 742, 743, 744, 745, 746, 747,
                748, 749, 750, 751, 752, 753, 754, 755, 756, 757, 758, 759, 760,
                761, 762, 763, 764, 765, 766, 767, 768, 769, 770, 771, 772, 773,
                774, 775, 776, 777, 778, 779, 780, 781, 782, 783, 784, 785, 786,
                787, 788, 789, 790, 791, 792, 793, 794, 795, 796, 797, 798, 799,
                800, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812,
                813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825,
                826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 838,
                839, 840, 841, 842, 843, 844, 845, 846, 847, 848, 849, 850, 851,
                852, 853, 854, 855, 856, 857, 858, 859, 860, 861, 862, 863, 864,
                865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 875, 876, 877,
                878, 879, 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890,
                891, 892, 893, 894, 895, 896, 897, 898, 899, 900, 901, 902, 903,
                904, 905, 906, 907, 908, 909, 910, 911, 912, 913, 914, 915, 916,
                917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929,
                930, 931, 932, 933, 934, 935, 936, 937, 938, 939, 940, 941, 942,
                943, 944, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955,
                956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968,
                969, 970, 971, 972, 973, 974, 975, 976, 977, 978, 979, 980, 981,
                982, 983, 984, 985, 986, 987, 988, 989, 990, 991, 992, 993, 994,
                995, 996, 997, 998, 999
              ],
              'operator': 'none',
              'field': null,
              'additionalValues': {
                'salt': '00cddb4b-69f5-47c6-aeaa-5bac43cf45a4'
              }
            }
          ],
          'returnValue': {
            'layer_param': true,
            'second_layer_param': false
          },
          'id': '',
          'salt': ''
        },
        {
          'name': '2RamGsERWbWMIMnSfOlQuX',
          'groupName': 'Control',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_bucket',
              'targetValue': 500,
              'operator': 'lt',
              'field': null,
              'additionalValues': {
                'salt': 'f8aeba58-18fb-4f36-9bbd-4c611447a912'
              }
            }
          ],
          'returnValue': {
            'experiment_param': 'control',
            'layer_param': true,
            'second_layer_param': false
          },
          'id': '2RamGsERWbWMIMnSfOlQuX',
          'salt': '2RamGsERWbWMIMnSfOlQuX'
        },
        {
          'name': '2RamGujUou6h2bVNQWhtNZ',
          'groupName': 'Test',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_bucket',
              'targetValue': 1000,
              'operator': 'lt',
              'field': null,
              'additionalValues': {
                'salt': 'f8aeba58-18fb-4f36-9bbd-4c611447a912'
              }
            }
          ],
          'returnValue': {
            'experiment_param': 'test',
            'layer_param': true,
            'second_layer_param': true
          },
          'id': '2RamGujUou6h2bVNQWhtNZ',
          'salt': '2RamGujUou6h2bVNQWhtNZ'
        }
      ]
    }
  ],
  'feature_gates': [
    {
      'name': 'always_on_gate',
      'type': 'feature_gate',
      'salt': '47403b4e-7829-43d1-b1ac-3992a5c1b4ac',
      'enabled': true,
      'defaultValue': false,
      'entity': 'feature_gate',
      'rules': [
        {
          'name': '6N6Z8ODekNYZ7F8gFdoLP5',
          'groupName': 'everyone',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'public',
              'targetValue': null,
              'operator': null,
              'field': null,
              'additionalValues': {}
            }
          ],
          'returnValue': true,
          'id': '6N6Z8ODekNYZ7F8gFdoLP5',
          'salt': '14862979-1468-4e49-9b2a-c8bb100eed8f'
        }
      ]
    },
    {
      'name': 'on_for_statsig_email',
      'type': 'feature_gate',
      'salt': '4ab7fc7b-c8a0-4ef1-b869-889467678688',
      'enabled': true,
      'defaultValue': false,
      'entity': 'feature_gate',
      'rules': [
        {
          'name': '7w9rbTSffLT89pxqpyhuqK',
          'groupName': 'on for statsig emails',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_field',
              'targetValue': ['@statsig.com'],
              'operator': 'str_contains_any',
              'field': 'email',
              'additionalValues': {}
            }
          ],
          'returnValue': true,
          'id': '7w9rbTSffLT89pxqpyhuqK',
          'salt': 'e452510f-bd5b-42cb-a71e-00498a7903fc'
        }
      ]
    },
    {
      'name': 'on_for_id_list',
      'type': 'feature_gate',
      'salt': '4ab7fc7b-c8a0-4ef1-b869-8894676786aa',
      'enabled': true,
      'defaultValue': false,
      'entity': 'feature_gate',
      'rules': [
        {
          'name': '7w9rbTSffLT89pxqpyhuqA',
          'groupName': 'on for people in id list',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_field',
              'targetValue': 'list_1',
              'operator': 'in_segment_list',
              'field': 'userID',
              'additionalValues': {}
            }
          ],
          'returnValue': true,
          'id': '7w9rbTSffLT89pxqpyhuqA',
          'salt': 'e452510f-bd5b-42cb-a71e-00498a7903fD'
        }
      ]
    }
  ],
  'layer_configs': [
    {
      'name': 'a_layer',
      'type': 'dynamic_config',
      'salt': 'f8aeba58-18fb-4f36-9bbd-4c611447a912',
      'enabled': true,
      'defaultValue': { 'experiment_param': 'control' },
      'rules': [
        {
          'name': 'experimentAssignment',
          'groupName': 'Experiment Assignment',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'user_bucket',
              'targetValue': [
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
                18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
                34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65,
                66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81,
                82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97,
                98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
                111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123,
                124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136,
                137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
                150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162,
                163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175,
                176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188,
                189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201,
                202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214,
                215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227,
                228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240,
                241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253,
                254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266,
                267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279,
                280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292,
                293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305,
                306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318,
                319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331,
                332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344,
                345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357,
                358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370,
                371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383,
                384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396,
                397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409,
                410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422,
                423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435,
                436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448,
                449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461,
                462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474,
                475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487,
                488, 489, 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500,
                501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513,
                514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526,
                527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539,
                540, 541, 542, 543, 544, 545, 546, 547, 548, 549, 550, 551, 552,
                553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565,
                566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578,
                579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591,
                592, 593, 594, 595, 596, 597, 598, 599, 600, 601, 602, 603, 604,
                605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616, 617,
                618, 619, 620, 621, 622, 623, 624, 625, 626, 627, 628, 629, 630,
                631, 632, 633, 634, 635, 636, 637, 638, 639, 640, 641, 642, 643,
                644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654, 655, 656,
                657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 667, 668, 669,
                670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682,
                683, 684, 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695,
                696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708,
                709, 710, 711, 712, 713, 714, 715, 716, 717, 718, 719, 720, 721,
                722, 723, 724, 725, 726, 727, 728, 729, 730, 731, 732, 733, 734,
                735, 736, 737, 738, 739, 740, 741, 742, 743, 744, 745, 746, 747,
                748, 749, 750, 751, 752, 753, 754, 755, 756, 757, 758, 759, 760,
                761, 762, 763, 764, 765, 766, 767, 768, 769, 770, 771, 772, 773,
                774, 775, 776, 777, 778, 779, 780, 781, 782, 783, 784, 785, 786,
                787, 788, 789, 790, 791, 792, 793, 794, 795, 796, 797, 798, 799,
                800, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812,
                813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825,
                826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 838,
                839, 840, 841, 842, 843, 844, 845, 846, 847, 848, 849, 850, 851,
                852, 853, 854, 855, 856, 857, 858, 859, 860, 861, 862, 863, 864,
                865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 875, 876, 877,
                878, 879, 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890,
                891, 892, 893, 894, 895, 896, 897, 898, 899, 900, 901, 902, 903,
                904, 905, 906, 907, 908, 909, 910, 911, 912, 913, 914, 915, 916,
                917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929,
                930, 931, 932, 933, 934, 935, 936, 937, 938, 939, 940, 941, 942,
                943, 944, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955,
                956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968,
                969, 970, 971, 972, 973, 974, 975, 976, 977, 978, 979, 980, 981,
                982, 983, 984, 985, 986, 987, 988, 989, 990, 991, 992, 993, 994,
                995, 996, 997, 998, 999
              ],
              'operator': 'any',
              'field': null,
              'additionalValues': {
                'salt': '58d96daa-4d16-467a-b616-7232c45153f4'
              },
              'isDeviceBased': false,
              'idType': 'userID'
            }
          ],
          'returnValue': {
            'layer_param': true,
            'second_layer_param': false
          },
          'id': 'experimentAssignment',
          'salt': '',
          'isDeviceBased': false,
          'idType': 'userID',
          'configDelegate': 'sample_experiment'
        }
      ],
      'isDeviceBased': false,
      'idType': 'userID',
      'entity': 'layer'
    },
    {
      'name': 'b_layer_no_alloc',
      'type': 'dynamic_config',
      'salt': '3e361046-bc69-4dfd-bbb1-538afe609157',
      'enabled': true,
      'defaultValue': {
        'b_param': 'layer_default'
      },
      'rules': [],
      'isDeviceBased': false,
      'idType': 'userID',
      'entity': 'layer'
    },
    {
      'name': 'c_layer_with_holdout',
      'type': 'dynamic_config',
      'salt': 'ab40c4af-947f-411e-9403-880393703507',
      'enabled': true,
      'defaultValue': {
        'holdout_layer_param': 'layer_default'
      },
      'rules': [
        {
          'name': '7d2E854TtGmfETdmJFip1L',
          'groupName': 'master_holdout',
          'passPercentage': 100,
          'conditions': [
            {
              'type': 'pass_gate',
              'targetValue': 'always_on_gate',
              'operator': 'any',
              'field': null,
              'additionalValues': null,
              'isDeviceBased': false,
              'idType': 'userID'
            }
          ],
          'returnValue': {
            'holdout_layer_param': 'layer_default'
          },
          'id': '7d2E854TtGmfETdmJFip1L',
          'salt': '',
          'isDeviceBased': false,
          'idType': 'userID'
        }
      ],
      'isDeviceBased': false,
      'idType': 'userID',
      'entity': 'layer'
    }
  ],
  'has_updates': true,
  'time': 1631638014811,
  'id_lists': { 'list_1': true, 'list_2': true }
}

			";
    }
}

