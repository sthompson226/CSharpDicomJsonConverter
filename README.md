CSharpDicomJsonConverter
========================
Manabu Tokunaga

# Latest Update
## 30 October 2014

* It can now include the DICOM dictionary definition optionally when generating JSON as "desc:" field
* Release build mode added and fixed the target CPU to Any
* This kit now supports conversion in both from DICOM to JSON, and also JSON to DICOM directions.
* Added Base64 Encoding of OB, OW, and OF data buffers. Though I would not recommend storing large files in JSON.

# About This Project

This kit contains the code to inter-convert a DICOM stream to and from a JSON format text stream. The primary use of this product is for supporting AJAX communications to/from JavaScript based code implementations such as web pages and Node.js packages.

This kit is mainly intended to work with non-image DICOM data such as Structured Reports and also for presenting C-FIND data via the DICOM Web standard such as QIDO. Everything is done in memory, so it is not advisable to parse a huge DICOM object.

In order to prevent conversion of large chunk of data, the system defaults not to output data that are more than 1024 bytes in length by default. This can be overridden.

The output should meet the QIDO-RS SearchForStudies response, which is described at ftp://medical.nema.org/medical/dicom/2013/output/chtml/part18/sect_F.4.html


# Requirements, Build:

* Use the Visual Studio 2013
* Add NuGet so that fo-dicom is pulled from the NuGet system.
* This project is targeted to .NET Framework 4.5. It can probably go to .NET 4.0, a fo-dicom dependency.

# Questions and Bug Reports

* Issues should be brought up on the fo-dicom Google Groups postings.
* Bugs should be reported on the issue reporting system on this GitHub Repo
