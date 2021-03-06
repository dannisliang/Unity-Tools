﻿using System;
using UnityEngine;
using System.Collections;

namespace VNToolkit.VNEditor.VNUtility {

	public interface VNIData {

		// Public Variables
		int DATA_ID { get; set; }

		string DATA_TITLE { get; }

		string BASE_PATH { get; }

		string FILE_EXT { get; }

		string FILE_FORMAT { get; }
	}
}