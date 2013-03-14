#import <Foundation/Foundation.h>
#import "Fraction.h"

int main (int argc, char *argv[])
{
	@autoreleasepool
	{
		Fraction *aFraction = [[Fraction alloc] init];
		Fraction *bFraction = [[Fraction alloc] init];
		Fraction *cFraction = [[Fraction alloc] init];

		[aFraction setNumerator: 1];
		[aFraction setDenominator: 5];
		
		[aFraction print];

		NSLog(@" =");
		NSLog(@"%g", [aFraction convertToNum]);

		[bFraction setNumerator: 4 andDenominator: 5];

		[bFraction print];

		NSLog(@" =");
		NSLog(@"%g", [bFraction convertToNum]);
	}

	return 0;
}