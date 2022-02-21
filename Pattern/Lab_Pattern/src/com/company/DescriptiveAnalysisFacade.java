package com.company;

public class DescriptiveAnalysisFacade {

    private AverageValues averageValues;
    private PercentilesDistribution percentilesDistribution;
    private DispersionIndicators dispersionIndicators;
    {
        averageValues=new AverageValues();
        percentilesDistribution=new PercentilesDistribution();
        dispersionIndicators=new DispersionIndicators();
    }

    public void MakeDescriptiveAnalysis() {
        averageValues.Search(new AverageSample());
        averageValues.Search(new Median());
        averageValues.Search(new Mode());

        percentilesDistribution.Search(new QuartileValues());

        dispersionIndicators.Search(new Variance_StandardDeviation());
    }
}
