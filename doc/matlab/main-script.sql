/*SELECT count(*)
FROM faceRecognition.database_matlab_2 AS ml
  LEFT JOIN faceRecognition.database_source_2 AS src ON(ml.testImageName = src.testImageName)
WHERE 
  ml.Euc_dist_min < 1.91E16
    AND ml.trainImageName = src.trainImageName
ORDER BY cast((replace(ml.testImageName,'.bmp','')) AS SIGNED);*/

/*SET 1.91E16 = 3.39e16;
SET @FMR = 0;
SET @CORRECT = 0;*/

/* FAR 
*/

SET @FAR = (
	SELECT count(Temp1.testImageName)/500*100 AS FAR
    FROM (
		SELECT ml.testImageName
		FROM faceRecognition.database_matlab_1_1 AS ml
		WHERE 
			ml.Euc_dist_min < 2.39e16
		GROUP BY ml.testImageName
		ORDER BY cast((replace(ml.testImageName,'.bmp','')) AS SIGNED)
    ) AS Temp1
);

SELECT @FAR;

/* FNMR
 */
SET @FNMR =  (
	SELECT count(Temp1.testImageName)/500*100 AS FNMR
	FROM
		( 
			SELECT ml.testImageName
			FROM faceRecognition.database_matlab_2 AS ml
				LEFT JOIN faceRecognition.database_source_2 AS src ON(ml.testImageName = src.testImageName)
			WHERE 
				ml.Euc_dist_min > 1.91E16
				AND ml.trainImageName = src.trainImageName
			GROUP BY ml.testImageName
			ORDER BY cast((replace(ml.testImageName,'.bmp','')) AS SIGNED)
		) AS Temp1
);

/* CORRECT */
SET @CORRECT = (
	SELECT count(Temp1.testImageName)/500*100 AS CORRECT
	FROM
		( 
			SELECT ml.testImageName
			FROM faceRecognition.database_matlab_2 AS ml
				LEFT JOIN faceRecognition.database_source_2 AS src ON(ml.testImageName = src.testImageName AND ml.trainImageName = src.trainImageName)
			WHERE 
				ml.Euc_dist_min < 1.91E16
                AND ml.trainImageName = src.trainImageName
			GROUP BY ml.testImageName
			ORDER BY cast((replace(ml.testImageName,'.bmp','')) AS SIGNED)
		) AS Temp1
);

/* FRR
 */
SET @FRR =  (
	SELECT count(Temp1.testImageName)/500*100 AS FRR
	FROM
		( 
			SELECT ml1.testImageName
			FROM faceRecognition.database_matlab_2 AS ml1
				LEFT JOIN faceRecognition.database_source_2 AS src ON(ml1.testImageName = src.testImageName)
			WHERE 
				ml1.Euc_dist_min < 1.91E16
				AND NOT EXISTS(
					SELECT ml.testImageName
					FROM faceRecognition.database_matlab_2 AS ml
						LEFT JOIN faceRecognition.database_source_2 AS src ON(ml.testImageName = src.testImageName AND ml.trainImageName = src.trainImageName)
					WHERE 
						ml.Euc_dist_min < 1.91E16
						AND ml.trainImageName = src.trainImageName
						AND ml.testImageName = ml1.testImageName
					ORDER BY cast((replace(ml.testImageName,'.bmp','')) AS SIGNED)
				)
			GROUP BY ml1.testImageName
			ORDER BY cast((replace(ml1.testImageName,'.bmp','')) AS SIGNED)
		) AS Temp1
);

/* FAR 
*/


SELECT CONCAT(@FNMR, ' | ', @CORRECT, ' | ', @FRR) AS 'FNMR | CORRECT | FRR';
