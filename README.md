DATreconstruct
==============

For converting binarys back to simple DAT dasm instructions for use in emulators.

* Files must be in little endian format.
* Output file will be of the format:
  DAT 0x[byte2][byte1], 0x[byte4][byte3] .....


USAGE: $ DATreconstruct input.d16 output.txt